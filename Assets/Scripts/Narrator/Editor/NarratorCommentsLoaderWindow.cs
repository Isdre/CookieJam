using Codice.CM.WorkspaceServer.Tree.GameUI.HeadTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Narrator.Editor
{
    [EditorWindowTitle(title = "Narrator Comments Loader")]
    public class NarratorCommentsLoaderWindow : EditorWindow
    {
        private TextAsset commentsFile;
        private DefaultAsset targetFolder;
        private string targetPath;

        [MenuItem("Window/Narrator Comments Loader")]
        private static void ShowNarratorLoader()
        {
            var window = GetWindow<NarratorCommentsLoaderWindow>();
            window.Show();
        }

        private void OnGUI()
        {
            commentsFile = EditorGUILayout.ObjectField("Comments File", commentsFile, typeof(TextAsset), false) as TextAsset;
            targetFolder = (DefaultAsset)EditorGUILayout.ObjectField(
                        "Target Folder",
                        targetFolder,
                        typeof(DefaultAsset),
                        false);

            if (commentsFile != null && targetFolder != null)
            {
                if (GUILayout.Button("Load Comments"))
                {
                    targetPath = AssetDatabase.GetAssetPath(targetFolder) + "/";
                    LoadComments();
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Please specify the comments file and target folder", MessageType.Info);
            }
        }

        private void LoadComments()
        {
            string textContent = commentsFile.text;
            var lines = textContent
                .Split('\n')
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Trim())
                .ToArray();

            var commentBuilder = new NarratorCommentSequenceBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                var (commentName, section) = LoadCommentSectionFromLine(line);
                commentBuilder.Name ??= commentName;
                if (commentBuilder.Name != null && commentName != commentBuilder.Name)
                {
                    CreateAndSaveSequence();

                    commentBuilder.Clear();
                    commentBuilder.Name = commentName;
                }

                commentBuilder.AddComment(section);
            }

            CreateAndSaveSequence();
            AssetDatabase.SaveAssets();

            void CreateAndSaveSequence()
            {
                var comment = commentBuilder.Build();
                Debug.Log($"Building comment number {commentBuilder.Name}");
                AssetDatabase.CreateAsset(comment, targetPath + commentBuilder.Name + ".asset");
            }
        }

        private (string commentName, NarratorComment section) LoadCommentSectionFromLine(string line)
        {
            var sides = line.Split(';');
            if (sides.Length != 2)
                throw new Exception();

            string clipName = sides[0].Trim();
            string narratorCommentText = sides[1].Trim();

            int lastDotIndex = clipName.LastIndexOf('.');
            string commentName = clipName[..lastDotIndex];

            var audioClip = AssetDatabase.FindAssets("t:AudioClip " + clipName)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<AudioClip>)
                .FirstOrDefault();

            var section = new NarratorComment(audioClip, narratorCommentText);
            return (commentName, section);
        }
    }

    public class NarratorCommentSequenceBuilder
    {
        private FieldInfo sectionsFieldInfo = typeof(NarratorCommentSequence).GetField("comments", BindingFlags.NonPublic | BindingFlags.Instance);

        private readonly List<NarratorComment> comments = new List<NarratorComment>();

        public string Name {get; set; }

        public void Clear()
        {
            Name = null;
            comments.Clear();
        }

        public void AddComment(NarratorComment section)
        {
            comments.Add(section);
        }

        public NarratorCommentSequence Build()
        {
            if (Name == null)
                throw new Exception("Name is not set");

            var sequence = ScriptableObject.CreateInstance<NarratorCommentSequence>();
            sequence.name = Name;
            sectionsFieldInfo.SetValue(sequence, comments.ToArray());    
            return sequence;
        }
    }
}
