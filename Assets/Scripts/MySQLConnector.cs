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
    public Text loginError;

    [Header("Login Data")]
    public InputField login_mail;
    public InputField login_password;

    [Header("Buttons")]
    public Button register;
    public Button login;
    public Button getCars;

    private void Awake()
    {
        // register.onClick.AddListener(Register);
        // login.onClick.AddListener(Login);
        getCars.onClick.AddListener(GetAllCars);
    }

    private void Register()
    {
        if (CheckDataEmpty() && FormatPhoneNumber() && ComparePassword())
        {
            WWWForm regForm = new WWWForm();
            regForm.AddField("firstName", firstName.text);
            regForm.AddField("lastName", lastName.text);
            regForm.AddField("mail", mail.text);
            regForm.AddField("password", password.text);

            StartCoroutine(SendRequest("http://localhost/SushiDriver/php/register.php", regForm, RegisterCallback));
        }
    }

    private void Login()
    {
        WWWForm loginForm = new WWWForm();
        loginForm.AddField("login_mail", login_mail.text);
        loginForm.AddField("login_password", login_password.text);

        StartCoroutine(SendRequest("http://localhost/SushiDriver/php/login.php", loginForm, LoginCallback));
    }

    private void GetAllCars()
    {
        StartCoroutine(SendRequest("http://sushidriver/getCars.php", null, GetAllCarsCallback));
    }

    private IEnumerator SendRequest(string url, WWWForm form, System.Action<WWW> callback)
    {
        WWW www = form != null ? new WWW(url, form) : new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Ошибка: " + www.error);
            yield break;
        }

        callback(www);
    }

    private void RegisterCallback(WWW www)
    {
        if (www.text == "false")
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

        if (loaded.userData.Status == "true")
        {
            SceneManager.LoadScene("Profile");
        }
    }

    private void GetAllCarsCallback(WWW www)
    {
        Debug.Log("All Cars");
        if (www.text == "false")
        {
            Debug.Log("Error");
        }
        else
        {
            Debug.Log(www.text);
        }
    }

    private void LoginCallback(WWW www)
    {
        if (www.text == "false")
        {
            loginError.text = "Введены неверные данные";
        }
        else
        {
            GetUserData(www);
        }
    }

    private bool CheckDataEmpty()
    {
        InputField[] data = { firstName, lastName, phone, mail, password, confirmPassword };
        foreach (var item in data)
        {
            if (string.IsNullOrEmpty(item.text))
            {
                errorText.text = "Поля должны быть заполнены";
                return false;
            }
        }
        errorText.text = "";
        return true;
    }

    private bool FormatPhoneNumber()
    {
        Regex regex = new Regex(@"\d+");
        MatchCollection matches = regex.Matches(phone.text);
        string formattedNumber = "";

        string numbers = "";
        foreach (Match match in matches)
        {
            numbers += match.Value;
        }
        if (numbers.Length == 11)
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

    private bool ComparePassword()
    {
        if (password.text.Length < 8 || confirmPassword.text.Length < 8)
        {
            errorText.text = "Пароль должен быть больше 8 символов";
            return false;

        }
        else if (password.text != confirmPassword.text)
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
