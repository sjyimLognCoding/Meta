using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AvatarDressUp : MonoBehaviour
{
    public static AvatarDressUp instance { get; private set; }

    [SerializeField] TextMeshProUGUI fit_text;
    [SerializeField] TextMeshProUGUI type_text;
    [SerializeField] TextMeshProUGUI model_code_text;
    [SerializeField] TMP_Dropdown drop;

    [SerializeField] GameObject avatar_female;
    [SerializeField] GameObject avatar_male;

    [SerializeField] Mesh[] female_jacket_meshes;

    [SerializeField] Mesh[] male_shirt_meshes;

    string[] animation_types = new string[4] {
        "isIdle",
        "isWalk",
        "isWave",
        "isTPose"
    };

    public string avatar_gender = "male";
    public string avatar_type = "top";
    public string avatar_size = "normal";

    // [SerializeField] Mesh[] female_jacket_meshes;

    //     [SerializeField] Mesh[] female_jacket_meshes;

    [SerializeField] GameObject AnimationButtonGroup;
    [SerializeField] GameObject AnimationOption;

    private int DDValue(string str)
    {
        switch (str)
        {
            case "normal":
                return 0;
            case "big":
                return 1;
            case "small":
                return 2;
        }

        return -2;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        // AvatarDress(avatar_gender, avatar_type, avatar_size);
        drop.value = DDValue(avatar_size);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         AvatarDress("female", "top", "big");
    //     }
    // }

    public void AvatarDress(string avatar_gender, string outfit_type = "top", string outfit_size = "normal")
    {
        GameObject avatar_dressed = Instantiate(avatar(avatar_gender), Vector3.zero, Quaternion.identity);
        if (avatar_gender == "male")
        {
            model_code_text.text = "SH1101";
            SkinnedMeshRenderer skin = avatar_dressed.transform.Find("Top5").GetComponent<SkinnedMeshRenderer>();
            skin.sharedMesh = Outfit(avatar_gender, outfit_type, outfit_size);
            type_text.text = "Shirt";
        }

        else if (avatar_gender == "female")
        {
            model_code_text.text = "J0x011";
            SkinnedMeshRenderer skin = avatar_dressed.transform.Find("Top2").GetComponent<SkinnedMeshRenderer>();
            skin.sharedMesh = Outfit(avatar_gender, outfit_type, outfit_size);
            type_text.text = "Jacket";
        }

    }

    private GameObject avatar(string type)
    {
        switch (type)
        {
            case "female":
                return avatar_female;
            case "male":
            default:
                return avatar_male;
        }
    }

    public void ChangeAnimation(string animation)
    {
        Animator anim = GameObject.FindGameObjectWithTag("Avatar").GetComponent<Animator>();
        foreach (string str in animation_types)
        {
            anim.SetBool(str, false);
        }
        anim.SetBool(animation, true);

        AnimationButtonGroup.SetActive(false);
        AnimationOption.SetActive(true);
    }

    public void OpenAnimationOptions()
    {
        AnimationButtonGroup.SetActive(true);
        AnimationOption.SetActive(false);
    }

    public void onDropDownChanged(TMP_Dropdown dropdown)
    {
        if (avatar_gender == "male" && avatar_type == "top")
        {
            switch (dropdown.value)
            {
                case 0:
                    fit_text.text = "regular fit \n 100%";
                    fit_text.color = new Color(0, 0.85f, 0);
                    model_code_text.text = "SH1102";
                    GameObject.FindGameObjectWithTag("Avatar").transform.Find("Top5").GetComponent<SkinnedMeshRenderer>().sharedMesh = male_shirt_meshes[0];
                    break;
                case 1:
                    fit_text.text = "over fit \n125%";
                    fit_text.color = new Color(0.85f, 0f, 0);
                    model_code_text.text = "SH1103";
                    GameObject.FindGameObjectWithTag("Avatar").transform.Find("Top5").GetComponent<SkinnedMeshRenderer>().sharedMesh = male_shirt_meshes[1];
                    break;
                case 2:
                    fit_text.text = "slim fit \n90%";
                    fit_text.color = new Color(0, 0.85f, 0);
                    model_code_text.text = "SH1101";
                    GameObject.FindGameObjectWithTag("Avatar").transform.Find("Top5").GetComponent<SkinnedMeshRenderer>().sharedMesh = male_shirt_meshes[2];
                    break;
                case 3:
                default:
                    break;
            }
        }

        if (avatar_gender == "female" && avatar_type == "top")
        {
            switch (dropdown.value)
            {
                case 0:
                    fit_text.text = "regular fit \n 100%";
                    fit_text.color = new Color(0, 0.85f, 0);
                    model_code_text.text = "J0x012";
                    GameObject.FindGameObjectWithTag("Avatar").transform.Find("Top2").GetComponent<SkinnedMeshRenderer>().sharedMesh = female_jacket_meshes[0];
                    break;
                case 1:
                    fit_text.text = "over fit \n125%";
                    fit_text.color = new Color(0.85f, 0f, 0);
                    model_code_text.text = "J0x013";
                    GameObject.FindGameObjectWithTag("Avatar").transform.Find("Top2").GetComponent<SkinnedMeshRenderer>().sharedMesh = female_jacket_meshes[1];
                    break;
                case 2:
                    fit_text.text = "slim fit \n90%";
                    fit_text.color = new Color(0, 0.85f, 0);
                    model_code_text.text = "J0x011";
                    GameObject.FindGameObjectWithTag("Avatar").transform.Find("Top2").GetComponent<SkinnedMeshRenderer>().sharedMesh = female_jacket_meshes[2];
                    break;
                case 3:
                default:
                    break;
            }
        }
    }

    private Mesh Outfit(string gender, string type, string size)
    {
        if (gender == "male")
        {
            if (type == "top")
            {
                switch (size)
                {
                    case "normal":
                    default:
                        fit_text.text = "regular fit \n 100%";
                        fit_text.color = new Color(0, 0.85f, 0);
                        model_code_text.text = "SH1102";
                        return male_shirt_meshes[0];
                    case "big":
                        fit_text.text = "over fit \n125%";
                        fit_text.color = new Color(0.85f, 0f, 0);
                        model_code_text.text = "SH1103";
                        return male_shirt_meshes[1];
                    case "small":
                        fit_text.text = "slim fit \n90%";
                        fit_text.color = new Color(0, 0.85f, 0);
                        model_code_text.text = "SH1101";
                        return male_shirt_meshes[2];
                }
            }

            else
            {
                return null;
            }
        }

        else
        {
            if (type == "top")
            {
                switch (size)
                {
                    case "normal":
                    default:
                        fit_text.text = "regular fit \n 100%";
                        fit_text.color = new Color(0, 0.85f, 0);
                        model_code_text.text = "J0x012";
                        return female_jacket_meshes[0];
                    case "big":
                        fit_text.text = "over fit \n125%";
                        fit_text.color = new Color(0.85f, 0f, 0);
                        model_code_text.text = "J0x013";
                        return female_jacket_meshes[1];
                    case "small":
                        fit_text.text = "slim fit \n90%";
                        fit_text.color = new Color(0, 0.85f, 0);
                        model_code_text.text = "J0x011";
                        return female_jacket_meshes[2];
                }
            }

            else
            {
                return null;
            }
        }
    }


}