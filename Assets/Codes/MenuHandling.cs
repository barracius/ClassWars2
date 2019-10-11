using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class MenuHandling : MonoBehaviour
{
    private IDictionary dictFriend;
    private int counti;
    public GameObject cat;
    public Button profileButton;
    public InputField UsernameInputField;
    private static string OtherFriendCode;

    public Text friendCodeText;
    private FirebaseAuth _auth;
    private FirebaseUser _currentUser;
    public DatabaseReference reference;
    private int cuenta = 0;
    public RectTransform prefab;
    public ScrollRect scrollView;
    public RectTransform content;
    private List<ExampleItemView> views = new List<ExampleItemView>();

    public ArrayList friends = new ArrayList();
    public ArrayList friendsNames = new ArrayList();
    

    private void Awake()
    {

    }

    void Start()
    {
        _auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        _currentUser = _auth.CurrentUser;
        cat.SetActive(false);
        profileButton.GetComponentInChildren<Text>().text = _currentUser.DisplayName;
        friendCodeText.text = "Your friend code is: " + _currentUser.UserId;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        CheckFriendships(_currentUser.UserId);
        Debug.Log("ACA");
    }

    public void Profile_picPressed()
    {
        cat.SetActive(true);
    }

    public void Profile_close()
    {
        cat.SetActive(false);
    }

    public void onClickAddFriendButton()
    {
        OtherFriendCode = UsernameInputField.text;
        AddFriendDB();
    }

    public void Logout_btn()
    {
        _auth.SignOut();
        SceneManager.LoadScene("Scenes/LoginScene");
    }

    private void AddFriendDB()
    {
        Friendship friendship = new Friendship(_currentUser.UserId, OtherFriendCode,"Stand By");
        string json = JsonUtility.ToJson(friendship);
        reference.Child("friendship").Child(_currentUser.UserId + " | " + OtherFriendCode).SetRawJsonValueAsync(json);
    }

    public void CheckFriendships(string u)
    {
        var friendshipRefs = FirebaseDatabase.DefaultInstance.GetReference("friendship");
        friendshipRefs.OrderByChild("user2_id").EqualTo(u).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot friendship in snapshot.Children.ToArray())
                {
                    IDictionary dictFriend = (IDictionary) friendship.Value;
                    //Debug.Log(dictFriend["user1_id"] + "<- User1");
                    //Debug.Log(dictFriend["user2_id"] + "<- User2");
                    //Debug.Log(dictFriend["status"] + "<- status");
                    ArrayList asdtemp = new ArrayList();
                    asdtemp.Add(dictFriend["user1_id"]);
                    asdtemp.Add(dictFriend["user2_id"]);
                    asdtemp.Add(dictFriend["status"]);
                    friends.Add(asdtemp);
                }
                CheckFriendsNames();
            }
        });
    }

    public void CheckFriendsNames()
    {
        var userRefs = FirebaseDatabase.DefaultInstance.GetReference("user");
        Debug.Log("ACA2");
        Debug.Log(friends.ToArray()[1]);
        Debug.Log(friends.ToArray()[0]);
        foreach (ArrayList array in friends)
        {
            Debug.Log("ACA3");
            Debug.Log(array.Count);
            Debug.Log("what");
            Debug.Log(array.ToString());
            Debug.Log(array[1]);

            Debug.Log(array[0]);
            userRefs.OrderByChild("id").EqualTo(array[0].ToString()).GetValueAsync().ContinueWith(
                task =>
                {
                    Debug.Log("ACA4");
                    if (task.IsFaulted)
                    {
                        // Handle the error...
                        Debug.Log("Error");
                    }
                    else if (task.IsCompleted)
                    {
                        Debug.Log("ACA5");
                        DataSnapshot snapshot2 = task.Result;
                        foreach (DataSnapshot user in snapshot2.Children)
                        {
                            Debug.Log("ACA6");
                            IDictionary dictUser = (IDictionary) user.Value;
                            Debug.Log(dictUser["username"] + " <- Username");
                            Debug.Log(dictUser["id"] + " <- Id");
                            ArrayList asdtemp2 = new ArrayList();
                            asdtemp2.Add(dictUser["username"]);
                            Debug.Log(asdtemp2[0] + " Valor 1");
                            asdtemp2.Add(dictUser["id"]);
                            Debug.Log(asdtemp2[1] + " Valor 2");
                            friendsNames.Add(asdtemp2);
                        }
                    }
                });
        }
    }



    public void UpdateItems()
    {
        FetchItemModelFromServer(cuenta, OnReceivedNewModels);
    }

    void OnReceivedNewModels(ExampleItemModel[] models)
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);
        views.Clear();
        int i = 0;
        foreach (var model in models)
        {
            var instance = GameObject.Instantiate(prefab.gameObject, content, false) as GameObject;
            var view = InitializeItemView(instance, model);
            views.Add(view);
            ++i;
        }
    }

    ExampleItemView InitializeItemView(GameObject viewGameObject, ExampleItemModel model)
    {
        ExampleItemView view = new ExampleItemView(viewGameObject.transform);
        view.usernameText.text = model.username;
        return view;
    }

    void FetchItemModelFromServer(int count, Action<ExampleItemModel[]> onDone)
    {
        //var results = new ExampleItemModel[count];
        var results = friendsNames;
        for (int i = 0; i < count; ++i)
        {
            //results[i].username = i.ToString();
        }
        //onDone(results);
    }

    public class ExampleItemView
    { 
        public Text usernameText;
        public ExampleItemView(Transform rootView)
        {
            usernameText = rootView.Find("TitlePanel/UsernameText").GetComponent<Text>();
        }
    }

    public class ExampleItemModel
    {
        public string username;
    }
    //funcion debug
    public void Asdasd()
    {
        foreach (ArrayList array in friendsNames)
        {
            print(array[0]);
            print(array[1]);
            //print(array[2]);
        }
    }
}