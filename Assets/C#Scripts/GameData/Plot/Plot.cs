using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Plot List", menuName = "List/Plot List")]
public class Plot : ScriptableObject
{
    [System.Serializable]
    public class ExcerptText
    {
        public CharacterType CharacterType;//说话者
        [TextArea]
        public string Text;
        public float WaitTime;//对话等待时间
    }
    [System.Serializable]
    public class PlotExcerpt 
    {
        public List<ExcerptText> Excerpts;
    }
    public int CurrentIndex;
    public List<PlotExcerpt> PlotExcerpts = new List<PlotExcerpt>();
}
