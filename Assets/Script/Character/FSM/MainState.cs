//using UnityEngine;
//using System.Collections;
//using System;
//using System.Collections.Generic;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;

//public class MainState : State<Main>
//{

//    public static MainState instance;

//    /*构造函数单例化*/
//    public static MainState Instance()
//    {
//        if (instance == null)
//            instance = new MainState();

//        return instance;
//    }

//    public const float resizeSpeed = 5;
//    public const float minCamerSize = 2;

//    public const string RMBMenu_001 = "NewButton";
//    public const string RMBMenu_002 = "SaveButton";
//    public const string RMBMenu_003 = "TitleButton";
//    public const string RMBMenu_004 = "ExportButton";
//    public const string RMBMenu_005 = "ResetPaperButton";

//    public string ProgramPath;
//    public const string SaveDir = "Save";
//    public const string ExportDir = "Export";
//    public const string FileExName = ".Tri";

//    public float oneWidth = 2;
//    public float oneHeight = 2 * Mathf.Sin(Mathf.Deg2Rad * 60);

//    public bool loadInTitle = true;

//    public override void Enter(Main Entity)
//    {
//        //这里添加进入此状态时执行的代码
//        ProgramPath = Application.dataPath;
//        string[] _path = ProgramPath.Split('/');
//        string _temPath = null;
//        for(int i = 0;i<_path.Length - 1; i++)
//        {
//            _temPath += _path[i] + "/";
//        }
//        ProgramPath = _temPath.Substring(0, _temPath.Length - 1);

//    }

//    public override void Execute(Main Entity)
//    {
//        //这里添加持续此状态刷新代码

//    }
//}

////wait input paper size
//public class Main_WaitStart : State<Main>
//{

//    public static Main_WaitStart instance;

//    /*构造函数单例化*/
//    public static Main_WaitStart Instance()
//    {
//        if (instance == null)
//            instance = new Main_WaitStart();

//        return instance;
//    }


//    public override void Enter(Main Entity)
//    {
//        Entity.UI_CreatPaper.SetActive(true);//开启创建界面
//    }

//    public override void Exit(Main Entity)
//    {
//        Entity.UI_CreatPaper.SetActive(false);//关闭创建界面
//    }
//}

////初始化新场景
//public class Main_SetPaper : State<Main>
//{

//    public static Main_SetPaper instance;

//    /*构造函数单例化*/
//    public static Main_SetPaper Instance()
//    {
//        if (instance == null)
//            instance = new Main_SetPaper();

//        return instance;
//    }

//    public override void Enter(Main Entity)
//    {
//        Entity.PaperPanel = new GameObject("PaperPanel").transform;
//        int w = int.Parse(Entity.Input_Width.text);
//        int h = int.Parse(Entity.Input_Height.text);
//        Date.Init().ImageW = w;
//        Date.Init().ImageH = h;
//        Date.Init().FileName = Entity.Input_FileName.text;
//        List<List<Trixel>> tris = new List<List<Trixel>>();
//        for(int i = 0; i < w; i++)
//        {
//            tris.Add(new List<Trixel>());
//            for (int j = 0; j < h; j++)
//            {
//                Trixel t = MonoBehaviour.Instantiate<Trixel>(Entity.trixel);
//                t.transform.position = new Vector3((MainState.Instance().oneWidth / 2) * i, 0, MainState.Instance().oneHeight * j);
//                if (j % 2 == 0)
//                {
//                    if (i % 2 == 0)
//                    {
//                        t.transform.eulerAngles = new Vector3(0, 180, 0);
//                    }
//                }
//                else
//                {
//                    if (i % 2 != 0)
//                    {
//                        t.transform.eulerAngles = new Vector3(0, 180, 0);

//                    }
//                }

//                t.SetPosDate(i, j);
//                tris[i].Add(t);
//                t.transform.parent = Entity.PaperPanel;
//            }
//        }
//        Date.Init().trixelList = tris;

//        if(w >= 4 * h)
//        {
//            Entity.mainCamera.orthographicSize = w / (6.0f * ((float)Screen.height / (float)Screen.width));
//        }
//        else
//        {
//            Entity.mainCamera.orthographicSize = h;
//        }


//        Entity.PaperPanel.transform.position = new Vector3(-(MainState.Instance().oneWidth * w / 4 - MainState.Instance().oneWidth / 4), 0, -(MainState.Instance().oneHeight * h / 2 - MainState.Instance().oneHeight / 2));
//        Date.Init().PaperMidPos = Entity.PaperPanel.transform.position;//设置画布初始数据
//        Date.Init().CamerSourceSize = Entity.mainCamera.orthographicSize;//设置初始相机参数
//        Entity.GetFSM().ChangeState(Main_DrawWait.Instance());//创建好场地后跳转到绘制等待
//    }

//    public override void Exit(Main Entity)
//    {
//    }

//}

////Draw Wait
//public class Main_DrawWait : State<Main>
//{

//    public static Main_DrawWait instance;

//    /*构造函数单例化*/
//    public static Main_DrawWait Instance()
//    {
//        if (instance == null)
//            instance = new Main_DrawWait();

//        return instance;
//    }

//    float currentW, prveW;

//    public override void Enter(Main Entity)
//    {
//        //重置数据
//        currentW = 0;
//        prveW = 0;
//    }

//    public override void Execute(Main Entity)
//    {
//        //按下ctrl 并点击鼠标，拾取颜色
//        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
//        {
//            if (Input.GetMouseButton(0))
//            {
//                Ray ray = Entity.mainCamera.ScreenPointToRay(Input.mousePosition);
//                RaycastHit rayHit;
//                if (Physics.Raycast(ray, out rayHit, Entity.PaperLayer))
//                {
//                    Trixel t = rayHit.transform.gameObject.GetComponent<Trixel>();
//                    Date.Init().currentColor = t.GetColor();
//                }
//            }

//            if (Input.GetKeyDown(KeyCode.Z))
//            {
//                Date.RollBack();
//            }
//            if (Input.GetKeyDown(KeyCode.Y))
//            {
//                Date.Opre();
//            }
//        }
//        //没按Ctrl
//        else     
//        {
//            //点击鼠标开始绘图
//            if (Input.GetMouseButtonDown(0))
//            {
//                Entity.GetFSM().ChangeState(Main_Drawing.Instance());
//            }
//        }
        
        
//        //右键弹出菜单
//        if (Input.GetMouseButtonDown(1))
//        {
//            Entity.GetFSM().ChangeState(Main_RMBMenu.Instance());
//        }
//        //移动画布
//        if (Input.GetMouseButtonDown(2))
//        {
//            Entity.GetFSM().ChangeState(Main_MovePaper.Instance());
//        }


//        //画面缩放
//        currentW = Input.GetAxis("Mouse ScrollWheel");
//        if(currentW != prveW )
//        {
//            if(Entity.mainCamera.orthographicSize + currentW * MainState.resizeSpeed > MainState.minCamerSize)
//                Entity.mainCamera.orthographicSize += currentW * MainState.resizeSpeed;
//        }
//        prveW = currentW;
//    }

//    public override void Exit(Main Entity)
//    {
//    }

//}

////drawing
//public class Main_Drawing : State<Main>
//{

//    public static Main_Drawing instance;

//    /*构造函数单例化*/
//    public static Main_Drawing Instance()
//    {
//        if (instance == null)
//            instance = new Main_Drawing();

//        return instance;
//    }

//    CDraw commandDraw;

//    public override void Enter(Main Entity)
//    {
//        commandDraw = new CDraw();
//    }

//    public override void Execute(Main Entity)
//    {
//        //松开鼠标的时候返回等待绘图
//        if (Input.GetMouseButtonUp(0))
//        {
//            Entity.GetFSM().ChangeState(Main_DrawWait.Instance());
//        }

//        Ray ray = Entity.mainCamera.ScreenPointToRay(Input.mousePosition);
//        RaycastHit rayHit;
//        if(Physics.Raycast(ray,out rayHit,Entity.PaperLayer))
//        {
//            Trixel t = rayHit.transform.gameObject.GetComponent<Trixel>();
//            Color prveColor = t.GetColor();//修改前的颜色
//            bool b = t.SetColor(Date.Init().currentColor);//判断是否成功改色
//            if (b)//如果操作生效
//            {
//                TrixelState prveTS = new TrixelState((int)t.GetPos().x, (int)t.GetPos().y, prveColor);
//                TrixelState currentTS = new TrixelState((int)t.GetPos().x, (int)t.GetPos().y, t.GetColor());
//                commandDraw.SetState(currentTS, prveTS);//状态数据的更新
//            }
//        }
//    }

//    public override void Exit(Main Entity)
//    {
//        Date.Init().AddCommand(commandDraw);//完成行动的时候把命令添加到命令列表
//    }
//}

////RMB Menu
//public class Main_RMBMenu : State<Main>
//{

//    public static Main_RMBMenu instance;

//    /*构造函数单例化*/
//    public static Main_RMBMenu Instance()
//    {
//        if (instance == null)
//            instance = new Main_RMBMenu();

//        return instance;
//    }


//    public override void Enter(Main Entity)
//    {
//        Entity.RMBMenu.SetActive(true);
//        //float scale = Entity.mainCamera.orthographicSize;
//        //Entity.ColorSelector.transform.localScale = new Vector3(scale, scale, 1);
//        Vector3 pos = Entity.rightClickCamera.ScreenToWorldPoint(Input.mousePosition);
//        pos.z = 0.5f;
//        Entity.RMBMenu.transform.position = pos;
//    }

//    public override void Execute(Main Entity)
//    {
//        if (Input.GetMouseButtonDown(1))//点右键关掉右键菜单
//        {
//            Entity.GetFSM().ChangeState(Main_DrawWait.Instance());
//        }

//        //点击左键执行功能
//        if (Input.GetMouseButton(0))
//        {
//            Ray ray = Entity.rightClickCamera.ScreenPointToRay(Input.mousePosition);
//            RaycastHit rayHit;
//            if (Physics.Raycast(ray, out rayHit, Entity.RightClickMenueLayer))
//            {
//                switch (rayHit.transform.name)
//                {
//                    case MainState.RMBMenu_001:
//                        Entity.GetFSM().ChangeState(Main_NewProject.Instance());
//                        break;
//                    case MainState.RMBMenu_002:
//                        Entity.GetFSM().ChangeState(Main_Save.Instance());
//                        break;
//                    case MainState.RMBMenu_003:
//                        MainState.Instance().loadInTitle = false;
//                        Entity.GetFSM().ChangeState(Main_NewProject.Instance());
//                        break;
//                    case MainState.RMBMenu_004:
//                        Entity.GetFSM().ChangeState(Main_ExportImage.Instance());
//                        break;
//                    case MainState.RMBMenu_005:
//                        Entity.GetFSM().ChangeState(Main_ResetPaperPos.Instance());
//                        break;
//                }
//            }
//        }
//    }

//    public override void Exit(Main Entity)
//    {
//        Date.Init().currentColor = ColorSelector.GetColor();
//        Entity.RMBMenu.SetActive(false);
//    }
//}

////MovePaper
//public class Main_MovePaper : State<Main>
//{

//    public static Main_MovePaper instance;

//    /*构造函数单例化*/
//    public static Main_MovePaper Instance()
//    {
//        if (instance == null)
//            instance = new Main_MovePaper();

//        return instance;
//    }

//    Vector3 mouseCurrentMove, mousePrvMove;
//    Vector3 moveDelta = new Vector3();

//    public override void Enter(Main Entity)
//    {
//        mouseCurrentMove = Entity.mainCamera.ScreenToWorldPoint(Input.mousePosition);
//        mousePrvMove = mouseCurrentMove;
//    }

//    public override void Execute(Main Entity)
//    {
//        if (Input.GetMouseButtonUp(2))//松手返回等待绘画状态
//        {
//            Entity.GetFSM().ChangeState(Main_DrawWait.Instance());
//        }
//        mouseCurrentMove = Entity.mainCamera.ScreenToWorldPoint(Input.mousePosition);

//        //计算偏差量
//        moveDelta.x = mouseCurrentMove.x - mousePrvMove.x;
//        moveDelta.z = mouseCurrentMove.z - mousePrvMove.z;

//        Entity.PaperPanel.transform.position = Entity.PaperPanel.transform.position + moveDelta;

//        mousePrvMove = mouseCurrentMove;
//    }

//    public override void Exit(Main Entity)
//    {
//    }
//}

////ExportImage
//public class Main_ExportImage : State<Main>
//{

//    public static Main_ExportImage instance;

//    /*构造函数单例化*/
//    public static Main_ExportImage Instance()
//    {
//        if (instance == null)
//            instance = new Main_ExportImage();

//        return instance;
//    }
    
//    Vector3 sourcePos;//操作前的画布位置
//    int CurrentFrameNumber = 0;
//    int WaitFram = 3;
//    Action SaveImage;
//    RenderTexture saveTex;
//    Main _Entity;
//    public override void Enter(Main Entity)
//    {
//        sourcePos = Entity.PaperPanel.position;
//        CurrentFrameNumber = 0;
//        SaveImage += WaitFrameFun;

//        //设置renderTexture
//        int width = Mathf.CeilToInt((32 * (Date.Init().ImageW / 2 + 0.5f)));
//        int height = Mathf.CeilToInt((32 * (Mathf.Sin(Mathf.Deg2Rad * 60) * Date.Init().ImageH)));
//        saveTex = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
//        saveTex.antiAliasing = 8;

//        sourcePos = Entity.PaperPanel.position;
//        //暂时重置画布到画面中央
//        Entity.PaperPanel.position = Date.Init().PaperMidPos;
//        //设置截图用摄像机
//        Entity.saveTextureCamera.gameObject.SetActive(true);
//        Entity.saveTextureCamera.orthographicSize = Mathf.Sin(Mathf.Deg2Rad * 60) * Date.Init().ImageH;
//        if(Date.Init().ImageH > 1)
//        {
//            Entity.saveTextureCamera.rect = new Rect(0, 0, Date.Init().ImageW / 2 + 0.5f, Date.Init().ImageH * Mathf.Sin(Mathf.Deg2Rad * 60));
//        }
//        else
//        {
//            Entity.saveTextureCamera.rect = new Rect(0, 0, Date.Init().ImageW / 2, Date.Init().ImageH * Mathf.Sin(Mathf.Deg2Rad * 60));
//        }
//        Entity.saveTextureCamera.targetTexture = saveTex;
//        _Entity = Entity;
//    }

//    public override void Execute(Main Entity)
//    {
//        SaveImage();
//    }

//    public override void Exit(Main Entity)
//    {
//        CurrentFrameNumber = 0;
//        Entity.saveTextureCamera.gameObject.SetActive(false);
//        _Entity.PaperPanel.position = sourcePos;
//    }

//    void WaitFrameFun()
//    {
//        if(CurrentFrameNumber < WaitFram)
//        {
//            CurrentFrameNumber++;
//        }
//        else
//        {
//            SaveImage -= WaitFrameFun;
//            SaveImage += SaveImageToPng;
//        }
//    }

//    void SaveImageToPng()
//    {
//        Texture2D myTexture2D = new Texture2D(saveTex.width, saveTex.height, TextureFormat.ARGB32, false);
//        RenderTexture.active = saveTex;
//        myTexture2D.ReadPixels(new Rect(0, 0, saveTex.width, saveTex.height), 0, 0);
//        myTexture2D.Apply();
//        RenderTexture.active = null;
//        if (!System.IO.Directory.Exists(MainState.Instance().ProgramPath + "/" + MainState.ExportDir))
//        {
//            System.IO.Directory.CreateDirectory(MainState.Instance().ProgramPath + "/" + MainState.ExportDir);
//        }
//        System.IO.File.WriteAllBytes(MainState.Instance().ProgramPath + "/" + MainState.ExportDir + "/" + Date.Init().FileName + ".png", myTexture2D.EncodeToPNG());
//        _Entity.GetFSM().ChangeState(Main_DrawWait.Instance());
//    }
//}

////重置画布位置
//public class Main_ResetPaperPos : State<Main>
//{

//    public static Main_ResetPaperPos instance;

//    /*构造函数单例化*/
//    public static Main_ResetPaperPos Instance()
//    {
//        if (instance == null)
//            instance = new Main_ResetPaperPos();

//        return instance;
//    }

//    public override void Enter(Main Entity)
//    {
//        Entity.PaperPanel.transform.position = Date.Init().PaperMidPos;
//        Entity.mainCamera.orthographicSize = Date.Init().CamerSourceSize;
//        Entity.GetFSM().ChangeState(Main_DrawWait.Instance());
//    }

//    public override void Execute(Main Entity)
//    {
//    }

//    public override void Exit(Main Entity)
//    {
//    }
//}

////新建
//public class Main_NewProject : State<Main>
//{

//    public static Main_NewProject instance;

//    /*构造函数单例化*/
//    public static Main_NewProject Instance()
//    {
//        if (instance == null)
//            instance = new Main_NewProject();

//        return instance;
//    }

//    public override void Enter(Main Entity)
//    {
//        Date.Init().FileName = null;
//        MonoBehaviour.Destroy(Entity.PaperPanel.gameObject);
//        Date.Init().trixelList = new List<List<Trixel>>();
//        Date.commandList.Clear();
//        Date.rollBackCommand.Clear();
//        Entity.GetFSM().ChangeState(Main_WaitStart.Instance());
//    }

//    public override void Execute(Main Entity)
//    {
//    }

//    public override void Exit(Main Entity)
//    {
//    }
//}

////Save
//public class Main_Save : State<Main>
//{

//    public static Main_Save instance;

//    /*构造函数单例化*/
//    public static Main_Save Instance()
//    {
//        if (instance == null)
//            instance = new Main_Save();

//        return instance;
//    }

//    public override void Enter(Main Entity)
//    {
//        Debug.Log("Save");
//        SaveDate sd = new SaveDate();
//        sd.width = Date.Init().ImageW;
//        sd.height = Date.Init().ImageH;

//        List<List<SaveTriDate>> std = new List<List<SaveTriDate>>();
//        for(int i = 0; i < Date.Init().trixelList.Count; i++)
//        {
//            std.Add(new List<SaveTriDate>());
//            for (int j = 0; j < Date.Init().trixelList[i].Count; j++)
//            {
//                SaveTriDate _std = new SaveTriDate();
//                _std.r = Date.Init().trixelList[i][j].GetColor().r;
//                _std.g = Date.Init().trixelList[i][j].GetColor().g;
//                _std.b = Date.Init().trixelList[i][j].GetColor().b;
//                _std.a = Date.Init().trixelList[i][j].GetColor().a;

//                _std.x = (int)Date.Init().trixelList[i][j].GetPos().x;
//                _std.y = (int)Date.Init().trixelList[i][j].GetPos().y;
//                std[i].Add(_std);
//            }
//        }
//        sd.date = std;
//        string savePath = MainState.Instance().ProgramPath + "/" + MainState.SaveDir;

//        if (!Directory.Exists(savePath))
//        {
//            Directory.CreateDirectory(savePath);
//        }
//        using(FileStream fs = new FileStream(savePath + "/" + Date.Init().FileName + MainState.FileExName, FileMode.Create))
//        {
//            BinaryFormatter bf = new BinaryFormatter();
//            bf.Serialize(fs, sd);
//        }
//        Entity.GetFSM().ChangeState(Main_DrawWait.Instance());
//    }
//}

////Load
//public class Main_Load : State<Main>
//{

//    public static Main_Load instance;

//    /*构造函数单例化*/
//    public static Main_Load Instance()
//    {
//        if (instance == null)
//            instance = new Main_Load();

//        return instance;
//    }
//    public override void Enter(Main Entity)
//    {
//        string savePath = MainState.Instance().ProgramPath + "/" + MainState.SaveDir;
//        string fileName = Entity.Input_FileName.text;
//        if (File.Exists(savePath + "/" + fileName + MainState.FileExName))
//        {
//            SaveDate sd = new SaveDate();
//            using (FileStream fs = new FileStream(savePath + "/" + Entity.Input_FileName.text + ".Tri", FileMode.Open))
//            {
//                BinaryFormatter bf = new BinaryFormatter();
//                sd = (SaveDate)bf.Deserialize(fs);
//            }
//            //初始化场景
//            Date.Init().FileName = fileName;
//            //开始加载数据

//            Entity.PaperPanel = new GameObject("PaperPanel").transform;
//            int w = sd.width;
//            int h = sd.height;
//            Date.Init().ImageW = w;
//            Date.Init().ImageH = h;
//            Date.Init().FileName = Entity.Input_FileName.text;
//            List<List<Trixel>> tris = new List<List<Trixel>>();
//            for (int i = 0; i < w; i++)
//            {
//                tris.Add(new List<Trixel>());
//                for (int j = 0; j < h; j++)
//                {
//                    Trixel t = MonoBehaviour.Instantiate<Trixel>(Entity.trixel);
//                    t.transform.position = new Vector3((MainState.Instance().oneWidth / 2) * i, 0, MainState.Instance().oneHeight * j);
//                    if (j % 2 == 0)
//                    {
//                        if (i % 2 == 0)
//                        {
//                            t.transform.eulerAngles = new Vector3(0, 180, 0);
//                        }
//                    }
//                    else
//                    {
//                        if (i % 2 != 0)
//                        {
//                            t.transform.eulerAngles = new Vector3(0, 180, 0);

//                        }
//                    }

//                    t.SetPosDate(sd.date[i][j].x, sd.date[i][j].y);
//                    t.SetColor(new Color(sd.date[i][j].r, sd.date[i][j].g, sd.date[i][j].b, sd.date[i][j].a));//设置颜色
//                    tris[i].Add(t);
//                    t.transform.parent = Entity.PaperPanel;
//                }
//            }
//            Date.Init().trixelList = tris;

//            if (w >= 4 * h)
//            {
//                Entity.mainCamera.orthographicSize = w / (6.0f * ((float)Screen.height / (float)Screen.width));
//            }
//            else
//            {
//                Entity.mainCamera.orthographicSize = h;
//            }

//            Entity.PaperPanel.transform.position = new Vector3(-(MainState.Instance().oneWidth * w / 4 - MainState.Instance().oneWidth / 4), 0, -(MainState.Instance().oneHeight * h / 2 - MainState.Instance().oneHeight / 2));
//            Date.Init().PaperMidPos = Entity.PaperPanel.transform.position;//设置画布初始数据
//            Date.Init().CamerSourceSize = Entity.mainCamera.orthographicSize;//设置初始相机参数
//            Entity.GetFSM().ChangeState(Main_DrawWait.Instance());
//        }
//        else
//        {
//            Entity.GetFSM().ChangeState(Main_WaitStart.Instance());
//        }
//    }
//}