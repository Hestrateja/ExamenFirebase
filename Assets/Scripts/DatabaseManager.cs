using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DatabaseManager : MonoBehaviour
{
	DatabaseReference reference;
	private bool logged = false;
	public InputField inputFieldUsername;
	public InputField inputFieldPassword;
	public InputField loginUsername;
	public InputField loginPassword;
	void Start()
	{
		reference = FirebaseDatabase.DefaultInstance.RootReference;
	}
	public void SignUp()
	{
		UserControl user = new UserControl();
		user.username = inputFieldUsername.text;
		user.password = inputFieldPassword.text;
		if (user.username.Length == 0 || user.password.Length == 0)
		{
			Debug.Log("Por favor, rellene los campos");
		}
		else
		{
			reference.Child("User").Child(user.username).GetValueAsync().ContinueWith(
			task => {
				if (task.IsCompleted)
				{
					DataSnapshot snapshot = task.Result;
					if (snapshot.Exists == true)
					{
						if (inputFieldUsername.text == snapshot.Child("username").Value.ToString())
						{
							Debug.Log("El usuario ya existe");
						}
					}
					else
					{
						Debug.Log("El usuario no existe");
						string json = JsonUtility.ToJson(user);


						reference.Child("User").Child(user.username).SetRawJsonValueAsync(json).ContinueWith(
						task2 => {
							if (task2.IsCompleted)
							{
								Debug.Log("Se almacenó correctamente a Firebase");
							}
							else
							{
								Debug.Log("No se almacenó correctamente a Firebase");
							}
						});
					}
				}
				
			});
			
			
		}
		
	}
	public void ReadAllData() {
		reference.Child("User").GetValueAsync().ContinueWith(
			task => {
				if (task.IsCompleted)
				{
					Debug.Log("Se cargaron correctamente los datos de Firebase");
					DataSnapshot snapshot = task.Result;
					foreach (var user in snapshot.Children)
                    {
						Debug.Log(user.Child("username").Value);
						Debug.Log(user.Child("password").Value);
					}
				}
				else
				{
					Debug.Log("No se cargaron correctamente los datos de Firebase");
				}
			});
	}
	public void LoadGame()
	{
		SceneManager.LoadScene(1);
	}
	public void Login()
	{
		reference.Child("User").Child(loginUsername.text).GetValueAsync().ContinueWith(
			task => {
				if (task.IsCompleted)
				{
					DataSnapshot snapshot = task.Result;
					if(snapshot.Exists == true)
                    {
						if (loginPassword.text == snapshot.Child("password").Value.ToString())
						{
							Debug.Log("Bienvenido " + snapshot.Child("username").Value.ToString() + "!");
							logged = true;
							Debug.Log("Cambio de escena");
						}
						else
                        {
							Debug.Log("Contraseña incorrecta");
						}
                    }else
                    {
						Debug.Log("El usuario no existe");
					}
				}
				else
				{
					Debug.Log("No se cargaron correctamente los datos de Firebase");
				}
			});
	}
	void Update()
    {
		if (logged)
		{
			LoadGame();
		}
    }
}
