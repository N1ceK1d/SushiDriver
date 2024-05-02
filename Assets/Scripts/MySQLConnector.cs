using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class MySQLConnector : MonoBehaviour
{
    [Header("Register Data")]
    public InputField firstName;
    public InputField lastName;
    public InputField phone;
    public InputField mail;
    public InputField password;
    public InputField confirmPassword;
    public Text errorText;
    private string formattedNumber;
    
    [Header("Login Data")]
    public InputField login_mail;
    public InputField login_password;
    public Text loginError;

    private bool FormatPhoneNumber()
    {
		Regex regex = new Regex(@"\d+");
        MatchCollection matches = regex.Matches(phone.text);

		string numbers = "";
		foreach (Match match in matches)
        {
            numbers += match.Value;
        }
		if(numbers.Length == 11)
		{
			formattedNumber = "+7 (" + numbers.Substring(1, 3) + ") " + numbers.Substring(4, 3) + " " + numbers.Substring(7, 2) + "-" + numbers.Substring(9, 2);
			errorText.text = "";
			return true;
		}
		else 
		{
			errorText.text = "Поле должно содержать 11 цифр";
			return false;
		}
    }

	public void Register()
	{
		if(CheckDataEmpty())
		{
			if(FormatPhoneNumber())
			{
				if(ComparePassword())
				{
					WWWForm regForm = new WWWForm();
					regForm.AddField("firstName", firstName.text);
					regForm.AddField("lastName", lastName.text);
					regForm.AddField("mail", mail.text);
					regForm.AddField("phone", formattedNumber);
					regForm.AddField("password", password.text);

					WWW www = new WWW("http://sushidriver/php/register.php", regForm);
					StartCoroutine(RegisterFunc(www));
				}
			}
		}
	}

	public void Login()
	{
		WWWForm loginForm = new WWWForm();
		loginForm.AddField("login_mail", login_mail.text);
		loginForm.AddField("login_password", login_password.text);

		WWW www = new WWW("http://sushidriver/php/login.php", loginForm);
		StartCoroutine(LoginFunc(www));
	}

	private IEnumerator RegisterFunc(WWW www)
	{
		yield return www;

		if(www.error != null)
		{
			Debug.Log("Ошибка: " + www.error);
			yield break;
		}
		
		if(www.text == "false")
		{
			errorText.text = "Такой пользователь уже существует";
		}
		else 
		{
			GetUserData(www);
		}
	}

	private void GetUserData(WWW www)
	{
		var text = www.text;
		Debug.Log(text);
		jsonDataClass loaded = JsonUtility.FromJson<jsonDataClass>(text);

		if(loaded.userData.Status == "true")
		{
			PlayerPrefs.SetInt("user_id", loaded.userData.id);
			PlayerPrefs.SetString("FirstName", loaded.userData.Name);
			PlayerPrefs.SetString("SecondName", loaded.userData.SecondName);
			PlayerPrefs.SetInt("Scrote", loaded.userData.Score);
			PlayerPrefs.SetString("CarColor", loaded.userData.CarColor);
            
		}
	}

	private IEnumerator LoginFunc(WWW www)
	{
		yield return www;

		if(www.text == "false")
		{
			loginError.text = "Введены неверные данные";
		} else 
		{
			GetUserData(www);
		}

		if(www.error != null)
		{
			Debug.Log("Ошибка: " + www.error);
			yield break;
		}
	}

	private bool CheckDataEmpty()
	{
		InputField[] data = {firstName,
		lastName,
		phone,
		mail,
		password,
		confirmPassword};
		bool isNonEmpty = false;
		foreach (var item in data)
		{
			isNonEmpty = item.text == "" ? false : true;
		}
		if(!isNonEmpty)
		{
			errorText.text = "Поля должны быть заполнены";
			return false;
		}
		else 
		{
			errorText.text = "";
			return true;
		}
	}

	private bool ComparePassword()
	{
		if(password.text.Length < 8 || confirmPassword.text.Length < 8)
		{
			errorText.text = "Пароль должен быть больше 8 символов";
			return false;
			
		}
		else if(password.text != confirmPassword.text)
		{
			errorText.text = "Пароли не совпадают";
			return false;
		} 
		else 
		{
			errorText.text = "";
			return true;
		}
	}
}

[System.Serializable]
public class jsonDataClass
{
    public UserData userData;
}

[System.Serializable]
public class UserData
{
	public int id;
    public string Name;
    public string SecondName;
    public string NumberPhone;
	public string Email;
	public int Score;
	public string CarColor;
	public string Status;
	public string Metallic;
	public string Smoothness;
}