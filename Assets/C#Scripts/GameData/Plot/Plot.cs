using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Plot List", menuName = "List/Plot List")]
public class Plot : ScriptableObject
{
    [System.Serializable]
    public class ExcerptText
    {
        public CharacterType CharacterType;//˵����
        [TextArea]
        public string Text;
        public float WaitTime;//�Ի��ȴ�ʱ��
    }
    [System.Serializable]
    public class PlotExcerpt 
    {
        public List<ExcerptText> Excerpts;
    }
    public int CurrentIndex;
    public List<PlotExcerpt> PlotExcerpts = new List<PlotExcerpt>();
}
