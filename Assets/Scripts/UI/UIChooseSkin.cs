using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

using TMPro;

using Ensign.Unity;
using Ensign.Tween;

public class UIChooseSkin : UIBase
{
    [Header("Buttons")]
    [SerializeField] private Button _buttonBack;

    [Header("Skins")]
    [SerializeField] protected SkinDataBase _skinDB;
    [SerializeField] private TMP_Text _indexSkin;
    [SerializeField] private TMP_Text _nameSkin;
    [SerializeField] private GameObject _skinSample;
    [SerializeField] private GameObject _listSkins;
    [SerializeField] private List<GameObject> _listSkinsCreated;

    [Header("Scroll")]
    [SerializeField] private ScrollRect _scrollView;
    [SerializeField] private GameObject _centeredObject;
    [SerializeField] private RectTransform _contentListSkin;
    private TMP_Text _valueSkin;

    [Header("UI")]
    [SerializeField] private CanvasGroup _choosseSkin;

    [Header("Value")]
    [SerializeField] private float _timeDelayTurnOffObject = 0.5f;
    protected int _selectSkin = 0;
    private Vector3 _valueScale3DObjectDefaut = new Vector3(150f, 150f, 150f);
    private float _maxValueCenterObject = 100f;
    private float _speedCenterObject = 10f;

    private void Awake() 
    {
        _choosseSkin.alpha = 0f;
        // Create emty list
        if(_listSkinsCreated.Count == 0)
            _listSkinsCreated.Add(_skinDB.GetSkins(0).skin);

        this._scrollView.onValueChanged.AddListener(delegate {GetAndSetObjectInCenterScreen(); });
    }
    private void Start() 
    {
        this._buttonBack.onClick.AddListener(TurnOffObject);
    }
    private void OnEnable() 
    {
        CreateListSkin();
        ChangeAlpha(_choosseSkin, 1f, _timeDelayTurnOffObject);

        _selectSkin = PlayerPrefs.GetInt("selectSkin");
        foreach (Transform child in _scrollView.content.transform)
        {
            if(_selectSkin == Int32.Parse(child.Find("IndexSkin").gameObject.GetComponent<TMP_Text>().text))
            {
                _centeredObject = child.Find("3DObject").gameObject;
            }
        }
        this.ActionWaitTime(0.01f, () => {
            _scrollView.FocusOnItem((RectTransform)_centeredObject.transform);
        });

        // _valueScale3DObjectDefaut = _scrollView.content.Find("3DObject").gameObject.transform.localScale;
        // Debug.Log(_valueScale3DObjectDefaut);
    }
    private void CreateListSkin()
    {
        for(int i=1; i < _skinDB.SkinsCount; i++)
        {
            if(!_listSkinsCreated.Contains(_skinDB.GetSkins(i).skin) && _skinDB.GetSkins(i).buy)
            {
                _indexSkin.text = ""+i;
                _nameSkin.text = "";
                Instantiate(_skinSample, _skinSample.transform.position, Quaternion.identity, _listSkins.transform);
                
                _listSkinsCreated.Add(_skinDB.GetSkins(i).skin);
            }
        }
        
        // reset value to default
        _indexSkin.text = "0";

        for(int i=0; i < _listSkins.transform.childCount; i++)
        {
            _listSkins.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void GetAndSetObjectInCenterScreen()
    {

        // Get the center point of the scroll view's viewport in world space
        Vector3 viewportCenterWorld = _scrollView.viewport.TransformPoint(_scrollView.viewport.rect.center);

        // Convert the center point to local space of the content
        Vector3 viewportCenterLocal = _scrollView.content.InverseTransformPoint(viewportCenterWorld);

        // Find the closest child GameObject to the center point
        float closestDistance = Mathf.Infinity;

        Vector3 scaleTo = new Vector3(50f, 50f, 50F);
        foreach (Transform child in _scrollView.content.transform)
        {
            float distance = Mathf.Abs(child.transform.localPosition.x - viewportCenterLocal.x);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                _centeredObject = child.Find("3DObject").gameObject;

                // Change scale object in center screen 
                // child.Find("3DObject").gameObject.transform.localScale = _valueScale3DObjectDefaut + new Vector3(30f, 30f, 30f);
                
                // Center object in scroll view
                if((Mathf.Abs(_scrollView.velocity.x) < _maxValueCenterObject) && (Input.touchCount == 0))
                {
                    StartCoroutine( _scrollView.FocusOnItemCoroutine( (RectTransform)_centeredObject.transform, _speedCenterObject ) );
                }
                _valueSkin = child.Find("IndexSkin").gameObject.GetComponent<TMP_Text>();
                PlayerPrefs.SetInt("selectSkin", Int32.Parse(_valueSkin.text));
            }

            // if(child.Find("IndexSkin").gameObject.GetComponent<TMP_Text>().text != _valueSkin.text)
            // {
            //     child.Find("3DObject").gameObject.transform.localScale = _valueScale3DObjectDefaut;
            // }
        }
    }
    private void TurnOffObject()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        ChangeAlpha(_choosseSkin, 0f, _timeDelayTurnOffObject);
        this.ActionWaitTime(_timeDelayTurnOffObject, () =>
        {
            ClosePopup(this.gameObject);
        });
    }
}
