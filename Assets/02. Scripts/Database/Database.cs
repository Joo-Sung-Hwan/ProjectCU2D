using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Database_", menuName = "Scriptable Objects/Database")]
public class Database : ScriptableObject
{
    [SerializeField] private List<IdentifiedObject> db = new();

    public IReadOnlyList<IdentifiedObject> DB => db;
    public int Count => db.Count;


    [ContextMenu("Sort By ID")] // 인스펙터에 정렬버튼 생성
    private void SortDB()
    {
        for (int i=0;i <db.Count; i++)
        {
            // id 필드는 public,static 변수가 아니여야함
            FieldInfo field = typeof(IdentifiedObject).GetField("id", BindingFlags.NonPublic | BindingFlags.Instance);

            // FieldInfo의 SetValue로 타겟의 id필드를 매개변수 설정
            field.SetValue(db[i], i);

            // SetDirty로 유니티에 수정됨을 알려주어야 실제로 저장됨
            #if UNITY_EDITOR
            EditorUtility.SetDirty(db[i]);
            #endif
        }

        #if UNITY_EDITOR
        // 데이터베이스 자체도 dirty로 표시
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        #endif        
    }


    public T GetDataByID<T>(int id) where T : IdentifiedObject    
        => db.FirstOrDefault(x => x.ID == id) as T;

    public bool Contains(IdentifiedObject item) 
        => db.Contains(item);
}