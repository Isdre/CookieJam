using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Narrator
{
    public interface INarratorComment
    {
        
    }

    [CreateAssetMenu]
    public class NarratorCommentSequence : ScriptableObject, INarratorComment
    {
        [SerializeField]
        private NarratorComment[] comments;
        public IReadOnlyList<NarratorComment> Comments => comments;  

        [ContextMenu("Say")]
        public void Say()
        {
            NarratorController.Instance.Say(this);
        }
    }
}
