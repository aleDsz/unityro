﻿using Assets.Scripts.Effects;
using ROIO;
using ROIO.Loaders;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Inspector
    [Header(":: Game Setup")]
    public bool OfflineOnly = false;

    [Header(":: Rendering Setup")]
    public AudioMixerGroup SoundMixerGroup;
    public Light WorldLight;
    #endregion

    private MapLoader MapLoader;
    private MapRenderer MapRenderer;
    private AudioSource AudioSource;

    #region Components
    private EntityManager EntityManager;
    private PathFinder PathFinder;
    #endregion

    public static Action OnGrfLoaded;
    public static Action OnMapLoaded;

    public Camera MainCamera { get; private set; }
    public bool IsMapReady => MapRenderer.Ready;
    public static long Tick => new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

    private Configuration Configs;

    private void Awake() {
        if (MainCamera == null) {
            MainCamera = Camera.main;
        }
        if (AudioSource == null) {
            AudioSource = gameObject.AddComponent<AudioSource>();
        }

        DontDestroyOnLoad(this);

        Instance = this;
    }

    private void OnEnable() {
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDisable() {
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera) {
        OnPostRender();
    }

    void Start() {
        Configs = ConfigurationLoader.Init();

        LoadGrf();
        DBManager.Init(Configs);
    }

    private void LoadGrf() {
        FileManager.LoadGRF(Configs.root, Configs.grf);
        OnGrfLoaded?.Invoke();

        InitManagers();
        MaybeInitOfflineUtils();

        MapRenderer = new MapRenderer(this, PathFinder, SoundMixerGroup, WorldLight);
        MapLoader = new MapLoader();
    }

    void FixedUpdate() {
        if (MapRenderer.Ready) {
            MapRenderer.FixedUpdate();
        }
    }

    void Update() {
        if (MainCamera == null) {
            MainCamera = Camera.main;
        }

        if (MapRenderer.Ready) {
            MapRenderer.Render();
        }
    }

    public void OnPostRender() {
        if (MapRenderer.Ready) {
            MapRenderer.PostRender();
        }
    }

    public void SetWorldLight(Light worldLight) {
        MapRenderer.WorldLight = worldLight;
    }

    public void InitCamera() {
        MainCamera = Camera.main;
    }

    public async void PlayBgm(string name) {
        var request = Resources.LoadAsync<AudioClip>(Path.Combine("Audio", "BGM", Path.GetFileNameWithoutExtension(name)));

        while(!request.isDone) {
            await Task.Yield();
        }

        AudioSource.clip = request.asset as AudioClip;
        //AudioSource.Play();
    }

    public async Task BeginMapLoading(string mapName) {
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);
        MapRenderer.Clear();
        EntityManager.ClearEntities();
        await MapLoader.Load($"{mapName}.rsw", MapRenderer.OnComplete);

        SceneManager.UnloadSceneAsync("LoadingScene");
        OnMapLoaded?.Invoke();

        PlayBgm(Tables.MapTable[$"{mapName}.rsw"].mp3);
    }

    public async Task<long> BenchmarkMapLoading(string mapName) {
        MapRenderer.Clear();
        EntityManager.ClearEntities();
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);
        var stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Restart();
        await MapLoader.Load($"{mapName}.rsw", MapRenderer.OnComplete);
        stopWatch.Stop();

        Debug.Log($"Map loaded in {stopWatch.Elapsed.TotalSeconds} seconds");
        SceneManager.UnloadSceneAsync("LoadingScene");

        return stopWatch.ElapsedMilliseconds;
    }

    //TODO Get rid of these
    #region Statics
    private static GameManager Instance;

    #endregion

    private void InitManagers() {
        new GameObject("ThreadManager").AddComponent<ThreadManager>();
        new GameObject("NetworkClient").AddComponent<NetworkClient>();
        EntityManager = new GameObject("EntityManager").AddComponent<EntityManager>();
        PathFinder = new GameObject("PathFinder").AddComponent<PathFinder>();
        new GameObject("CursorRenderer").AddComponent<CursorRenderer>();
        new GameObject("GridRenderer").AddComponent<GridRenderer>();
        new GameObject("ItemManager").AddComponent<ItemManager>();
    }

    private void MaybeInitOfflineUtils() {
        if (!OfflineOnly) {
            return;
        }

        gameObject.AddComponent<OfflineUtility>();
    }
}
