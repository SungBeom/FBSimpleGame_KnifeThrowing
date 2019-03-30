using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;
    public static GameManager GM
    {
        get { return gm; }
    }
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            GameObject tempGM = new GameObject("GameManager");
    //            instance = tempGM.AddComponent<GameManager>();
    //            DontDestroyOnLoad(tempGM);
    //        }
    //        return instance;
    //    }
    //}

    void Awake()
    {
        if (gm)
        {
            Destroy(gameObject);
            return;
        }
        gm = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
                InitializeFirebase();
            else
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        });
    }

    void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        // NOTE: You'll need to replace this url with your Firebase App's database
        // path in order for the database connection to work correctly in editor.
        app.SetEditorDatabaseUrl("https://knifethrowing-8bb08.firebaseio.com/");
        if (app.Options.DatabaseUrl != null)
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
    }

    private string nickname;
    public string Nickname
    {
        set { nickname = value; }
    }

    private int score;
    public int Score
    {
        set { score = value; }
    }

    private bool addCheck = true;
    public bool AddCheck
    {
        get { return addCheck; }
        set { addCheck = value; }
    }

    public void AddScore()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        reference.RunTransaction(AddScoreTransaction).ContinueWith(task =>
        {
            if (task.Exception != null)
                Debug.Log(task.Exception.ToString());
            else if (task.IsCompleted)
                Debug.Log("Transaction complete.");
        });
    }

    TransactionResult AddScoreTransaction(MutableData mutableData)
    {
        List<object> datas = mutableData.Value as List<object>;

        if (datas == null)
        {
            datas = new List<object>();
        }

        Dictionary<string, object> newData = new Dictionary<string, object>();
        newData[nickname] = score;
        datas.Add(newData);

        mutableData.Value = datas;
        return TransactionResult.Success(mutableData);
    }
}
