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

    private Dictionary<string, object> scores = new Dictionary<string, object>();
    public Dictionary<string, object> Scores
    {
        get { return scores; }
    }

    public void AddScore()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Score");

        reference.RunTransaction(AddScoreTransaction).ContinueWith(task =>
        {
            if (task.Exception != null)
                Debug.Log(task.Exception.ToString());
            else if (task.IsCompleted)
            {
                Debug.Log("Transaction complete.");
                GetScore();
            }
        });
    }

    TransactionResult AddScoreTransaction(MutableData mutableData)
    {
        Dictionary<string, object> datas = mutableData.Value as Dictionary<string, object>;
        if (datas == null) datas = new Dictionary<string, object>();

        datas[nickname] = score;

        mutableData.Value = datas;
        return TransactionResult.Success(mutableData);
    }

    public void GetScore()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Score");

        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
                Debug.Log(task.Exception.ToString());
            else if (task.IsCompleted)
            {
                Debug.Log("Task complete.");
                DataSnapshot snapshot = task.Result;
                scores = snapshot.Value as Dictionary<string, object>;
            }
        });
    }
}
