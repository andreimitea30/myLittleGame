// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using Firebase;
// using Firebase.Auth;
// using System;
// using System.Threading.Tasks;
// using Firebase.Extensions;

// public class FirebaseController : MonoBehaviour
// {
// 	public GameObject loginPanel, signupPanel, profilePanel, forgetpasswordPanel, notificationPanel;
// 	public InputField loginUsername, loginPassword, signupPassword, signupConfirmPassword, signupUsername;
// 	public Text notifTitleText, notifMessageText, profileUsernameText;
// 	public Toggle rememberMe;
//     Firebase.Auth.FirebaseAuth auth;
//     Firebase.Auth.FirebaseUser user;
//     Firebase.Auth.FirebaseUser newUser;
//     string displayName = "";
//     string photoUrl = "";

    
//     bool isSignIn = false;

//     void start() {
//         Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
//         var dependencyStatus = task.Result;
//         if (dependencyStatus == Firebase.DependencyStatus.Available) {
//             // Create and hold a reference to your FirebaseApp,
//             // where app is a Firebase.FirebaseApp property of your application class.
//             InitializeFirebase();

//             // Set a flag here to indicate whether Firebase is ready to use by your app.
//         } else {
//             UnityEngine.Debug.LogError(System.String.Format(
//             "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//             // Firebase Unity SDK is not safe to use here.
//         }
//         });
//     }
//     void OpenLoginPanel() {
//     loginPanel.SetActive(true);
//     signupPanel.SetActive(false);
//     profilePanel. SetActive(false);
//     }
//     void OpenSignUpPanel() {
//     loginPanel.SetActive(false);
//     signupPanel.SetActive(true);
//     profilePanel.SetActive(false);
//     }
//     void OpenProfilePanel() {
//     loginPanel.SetActive(false);
//     signupPanel. SetActive (false);
//     profilePanel.SetActive(true);
//     }
//     void OpenForgetPasswordPanel() {
//     loginPanel.SetActive(false);
//     signupPanel.SetActive(false);
//     profilePanel.SetActive(false);
//     }
//     void LoginUser () {
//     if (string.IsNullOrEmpty(loginUsername.text) && string.IsNullOrEmpty(loginPassword.text)) {
//         showNotificationMessage("Error", "Please enter username and password");
//         return;
//     }
//     SignInUser(loginUsername.text, loginPassword.text);
//     }

//     void SignUpUser () {
//     if (string.IsNullOrEmpty(signupUsername.text) && string.IsNullOrEmpty(signupPassword.text) && string.IsNullOrEmpty(signupConfirmPassword.text)) {
//         showNotificationMessage("Error", "Please enter all fields");
//         return;
//     }
//     if (signupPassword.text != signupConfirmPassword.text) {
//         showNotificationMessage("Error", "Password and confirm password must be same");
//         return;
//     }
//     CreateUser(signupPassword.text, signupUsername.text);
//     }

//     void showNotificationMessage(string title, string message) {
//     notifTitleText.text = "" + title;
//     notifMessageText.text = "" + message;
//     notificationPanel.SetActive(true);
//     }

//     void CloseNotif_Panel() {
//     notifTitleText.text = "";
//     notifMessageText.text = "";
//     notificationPanel.SetActive(false);
//     }

//     void Logout() {
//     auth.SignOut();
//     profilePanel.SetActive(false);
//     profileUsernameText.text = "";
//     OpenLoginPanel();
//     }

//     void CreateUser (string password, string UserName) {
//     auth.CreateUserWithEmailAndPasswordAsync(UserName + "@labozapp.com", password).ContinueWithOnMainThread(task => {
//     if (task.IsCanceled) {
//         Debug.LogError("CreateUserWithUsernameAndPasswordAsync was canceled.");
//         return;
//     }
//     if (task.IsFaulted) {
//         Debug.LogError("CreateUserWithUsernameAndPasswordAsync encountered an error: " + task.Exception);
//         foreach (Exception exception in task.Exception.Flatten().InnerExceptions) {
//             Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
//             if (firebaseEx != null) {
//                 var errorCode = (AuthError)firebaseEx.ErrorCode;
//                 showNotificationMessage("Error", GetErrorMessage(errorCode));
//             }
//         }
//         return;
//     }

//     // Firebase user has been created.
//     Firebase.Auth.AuthResult result = task.Result;
//     Debug.LogFormat("Firebase user created successfully: {0} ({1})",
//       result.User.DisplayName, result.User.UserId);
//       UpdateUserProfile(UserName);
//     });
//     }

//     void SignInUser (string username, string password) {
//     auth.SignInWithEmailAndPasswordAsync(username + "@labozapp.com", password).ContinueWithOnMainThread(task => {
//     if (task.IsCanceled) {
//         Debug.LogError("SignInWithUsernameAndPasswordAsync was canceled.");
//         return;
//     }
//     if (task.IsFaulted) {
//         Debug.LogError("SignInWithUsernameAndPasswordAsync encountered an error: " + task.Exception);
//         foreach (Exception exception in task.Exception.Flatten().InnerExceptions) {
//         Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
//         if (firebaseEx != null) {
//             var errorCode = (AuthError)firebaseEx.ErrorCode;
//            showNotificationMessage("Error", GetErrorMessage(errorCode));
//         }
//         }
//     return;
//   }

//   Firebase.Auth.AuthResult result = task.Result;
//   Debug.LogFormat("User signed in successfully: {0} ({1})",
//     result.User.DisplayName, result.User.UserId);
//     profileUsernameText.text = "" + newUser.DisplayName;
//     OpenProfilePanel();
//     });
//     }

//     void InitializeFirebase() {
//         auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
//         auth.StateChanged += AuthStateChanged;
//         AuthStateChanged(this, null);
//     }

//     void AuthStateChanged(object sender, System.EventArgs eventArgs) {
//   if (auth.CurrentUser != user) {
//     bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
//         && auth.CurrentUser.IsValid();
//     if (!signedIn && user != null) {
//       Debug.Log("Signed out " + user.UserId);
//       isSignIn = true;
//     }
//     user = auth.CurrentUser;
//     if (signedIn) {
//         Debug.Log("Signed in " + user.UserId);
//         displayName = user.DisplayName ?? "";
//     //   photoUrl = user.PhotoUrl ?? "";
//         photoUrl = user.PhotoUrl != null ? user.PhotoUrl.ToString() : "";
//     }
//   }
//     }

//     void OnDestroy() {
//   auth.StateChanged -= AuthStateChanged;
//   auth = null;
//     }

//     void UpdateUserProfile(String UserName) {
//     Firebase.Auth.FirebaseUser user = auth.CurrentUser;
//     if (user != null) {
//         Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile {
//         DisplayName = UserName,
//         PhotoUrl = new System.Uri("https://upload.wikimedia.org/wikipedia/commons/5/51/Mr._Smiley_Face.svg"),
//         };
//         user.UpdateUserProfileAsync(profile).ContinueWith(task => {
//             if (task.IsCanceled) {
//                 Debug.LogError("UpdateUserProfileAsync was canceled.");
//                 return;
//             }
//             if (task.IsFaulted) {
//                 Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
//                 return;
//             }

//         Debug.Log("User profile updated successfully.");
//         showNotificationMessage("Alert", "User created successfully");
//         });
//     }
//     }

//     void Update() {
//     if (isSignIn) {
//         isSignIn = false;
//         profileUsernameText.text = "" + user.DisplayName;
//         OpenLoginPanel();
//     }
//     }

//     string GetErrorMessage(AuthError errorCode) {
//     var message = "";
//     switch (errorCode)
//     {
//         case AuthError.AccountExistsWithDifferentCredentials:
//             message = "Account does not exist";
//             break;
//         case AuthError.MissingPassword:
//             message = "Missing password";
//             break;
//         case AuthError.WeakPassword:
//             message = "Password is too weak";
//             break;
//         case AuthError.WrongPassword:
//             message = "Wrong password";
//             break;
//         case AuthError.EmailAlreadyInUse:
//             message = "Username already in use";
//             break;
//         case AuthError.InvalidEmail:
//             message = "Invalid username";
//             break;
//         case AuthError.MissingEmail:
//             message = "Missing username";
//             break;
//         default:
//             message = "Unknown error occurred";
//             break;
//     }
//     return message;
//     }
// }