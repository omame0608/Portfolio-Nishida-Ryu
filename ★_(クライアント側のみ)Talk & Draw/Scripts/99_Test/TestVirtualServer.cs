using Cysharp.Threading.Tasks;
using GiikutenApplication.ForumScene.Data;
using GiikutenApplication.HomeScene.Data;
using GiikutenApplication.MessageScene.Data;
using GiikutenApplication.PackScene.Data;
using GiikutenApplication.SettingsScene.Data;
using System;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// テスト用：仮想サーバ
    /// データの送受信はDTO単位で行う
    /// テスト中はシングルトンオブジェクトとして保持
    /// </summary>
    public class TestVirtualServer : MonoBehaviour
    {
        //シングルトン
        public static TestVirtualServer Instance;

        //仮想サーバのステータス
        [SerializeField, Header("テスト：データ読み書きの処理時間(ミリ秒)")] private int _executionTime;
        [SerializeField, Header("テスト：読み込みに失敗する")] private bool _isReadFailed;
        [SerializeField, Header("テスト：書き込みに失敗する")] private bool _isWriteFailed;


        //永続化データ
        [Header("ユーザ1人当たりに必要な永続化データ")]
        [SerializeField] private string _userName; //ユーザの名前
        [SerializeField] private string _jobName; //ユーザのジョブ名
        [SerializeField] private int _gachaStoneAmount; //ガチャ石の所持数
        [SerializeField] private string _bloodType; //血液型
        [SerializeField] private int _height; //身長
        [SerializeField] private string _birthday; //誕生日
        [SerializeField] private string _favoriteWeather; //好きな天気
        [SerializeField] private string _favoriteColor; //好きな色
        [SerializeField] private string _dominantHand; //利き手
        [SerializeField] private string _text; //自己紹介
        
        //その他データ
        [Header("ランダムパック運用に必要なデータ")]
        [SerializeField] private string _randomUserName; //ランダムパックで排出されるユーザ名
        [SerializeField] private string _randomCharacterType; //ランダムパックで排出されるジョブ名
        [Header("レコメンドパック運用に必要なデータ")]
        [SerializeField] private string _recommendUserName; //レコメンドパックで排出されるユーザ名
        [SerializeField] private string _recommendCharacterType; //レコメンドパックで排出されるジョブ名
        [SerializeField] private string _advice; //アドバイス
        [Header("メッセージ機能運営に必要なデータ")]
        [SerializeField] private ChatInfo[] _chatInfoArray; //メッセージ出来るユーザの情報配列
        private MessageInDTO[][] _messageArray = new MessageInDTO[99][];
        [Header("けいじばん機能運営に必要なデータ")]
        [SerializeField] private ForumInfo[] _topicInfoArray; //メッセージ出来るトピックの情報配列
        private MessageInDTO[][] _topicArray = new MessageInDTO[99][];


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            //テストデータ構築用
            //user1との会話履歴
            _messageArray[0] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "user1の発言1", "https1", false, _chatInfoArray[0].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "自分の発言1", "じぶん", true, _userName),
                                                   new MessageInDTO(Guid.NewGuid(), "user1の発言2", "https1", false, _chatInfoArray[0].UserName)};
            //user2との会話履歴
            _messageArray[1] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "自分の発言1", "じぶん", true, _userName),
                                                   new MessageInDTO(Guid.NewGuid(), "user2の発言1", "https2", false, _chatInfoArray[1].UserName)};

            //user3との会話履歴
            _messageArray[2] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "user3の発言1", "https3", false, _chatInfoArray[2].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "自分の発言1", "じぶん", true, _userName),
                                                   new MessageInDTO(Guid.NewGuid(), "user3の発言2", "https3", false, _chatInfoArray[2].UserName)};

            //user4との会話履歴
            _messageArray[3] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "自分の発言1", "じぶん", true, _userName),
                                                   new MessageInDTO(Guid.NewGuid(), "user4の発言1", "https4", false, _chatInfoArray[3].UserName)};

            //user5との会話履歴
            _messageArray[4] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "かいはつつらい、しんどい、やめたい", "https5", false, _chatInfoArray[4].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "わかるしにそう", "じぶん", true, _userName),
                                                   new MessageInDTO(Guid.NewGuid(), "じっかにかえりたい", "https5", false, _chatInfoArray[4].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "あああああああああああああああああああああああああああああああああああああああああああああああ", "じぶん", true, _userName),
                                                   new MessageInDTO(Guid.NewGuid(), "だまれ", "https5", false, _chatInfoArray[4].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "ほんまだまれ", "https5", false, _chatInfoArray[4].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "うおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおお", "じぶん", true, _userName),};


            //Topic1の会話履歴
            _topicArray[0] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "user1の発言", "https1", false, _chatInfoArray[0].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "user2の発言", "https2", false, _chatInfoArray[1].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "user3の発言", "https3", false, _chatInfoArray[2].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "user4の発言", "https4", false, _chatInfoArray[3].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "user5の発言", "https5", false, _chatInfoArray[4].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "自分の発言", "じぶん", true, _userName)};
            //Topic2の会話履歴
            _topicArray[1] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "自分の発言", "じぶん", true, _userName),
                                                   new MessageInDTO(Guid.NewGuid(), "user2の発言", "https2", false, _chatInfoArray[1].UserName)};

            //Topic3の会話履歴
            _topicArray[2] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "user3の発言", "https3", false, _chatInfoArray[2].UserName),
                                                   new MessageInDTO(Guid.NewGuid(), "自分の発言", "じぶん", true, _userName),
                                                   new MessageInDTO(Guid.NewGuid(), "user3の発言", "https3", false, _chatInfoArray[2].UserName)};

            //Topic4の会話履歴
            _topicArray[3] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "自分の発言", "じぶん", true, _userName),
                                                   new MessageInDTO(Guid.NewGuid(), "user4の発言", "https4", false, _chatInfoArray[3].UserName)};

            //Topic5の会話履歴
            _topicArray[4] = new MessageInDTO[] {new MessageInDTO(Guid.NewGuid(), "自分の発言", "じぶん", true, _userName) };
        }


        /// <summary>
        /// HomeScene構築用のDTOを読み込む
        /// 読み込みに失敗した場合はnullが帰ってくる
        /// </summary>
        /// <returns>サーバのデータから作成したHomeScene構築用のDTO</returns>
        public async UniTask<HomeSceneDTO> GetHomeSceneDTO()
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            return new HomeSceneDTO(_userName, _jobName, _gachaStoneAmount);
        }


        /// <summary>
        /// HomeScene構築用のDTOを書き込む
        /// </summary>
        /// <param name="dto">クライアントから送られてきたHomeScene構築用のDTO</param>
        /// <returns>書き込みに成功したかどうか</returns>
        public async UniTask<bool> SetHomeSceneDTO(HomeSceneDTO dto)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならfalse
            if (_isWriteFailed) return false;

            //成功なら書き込み処理とtrue
            _userName = dto.UserName;
            _jobName = dto.JobName;
            _gachaStoneAmount = dto.GachaStoneAmount;
            return true;
        }


        /// <summary>
        /// SettingsScene構築用のDTOを読み込む
        /// 読み込みに失敗した場合はnullが帰ってくる
        /// </summary>
        /// <returns>サーバのデータから作成したSettingsScene構築用のDTO</returns>
        public async UniTask<SettingsInDTO> GetSettingsInDTO()
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            return new SettingsInDTO(_userName, _jobName, _bloodType, _height, _birthday, 
                                        _favoriteWeather, _favoriteColor, _dominantHand, _text);
        }


        /// <summary>
        /// SettingsScene構築用のDTOを書き込む
        /// </summary>
        /// <param name="dto">クライアントから送られてきたSettingsScene構築用のDTO</param>
        /// <returns>書き込みに成功したかどうか</returns>
        public async UniTask<bool> SetSettingsInDTO(SettingsInDTO dto)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならfalse
            if (_isWriteFailed) return false;

            //成功なら書き込み処理とtrue
            _bloodType = dto.BloodType;
            _height = dto.Height;
            _birthday = dto.Birthday;
            _favoriteWeather = dto.FavoriteWeather;
            _favoriteColor = dto.FavoriteColor;
            _dominantHand = dto.DominantHand;
            _text = dto.Text;
            return true;
        }


        /// <summary>
        /// PackScene構築用のDTOを読み込む
        /// 読み込みに失敗した場合はnullが帰ってくる
        /// </summary>
        /// <returns>サーバのデータから作成したPackScene構築用のDTO</returns>
        public async UniTask<PackScreenDTO> GetPackScreenDTO()
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            return new PackScreenDTO(_gachaStoneAmount);
        }


        /// <summary>
        /// PackScene構築用のDTOを書き込む
        /// </summary>
        /// <param name="dto">クライアントから送られてきたPackScene構築用のDTO</param>
        /// <returns>書き込みに成功したかどうか</returns>
        public async UniTask<bool> SetPackScreenDTO(PackScreenDTO dto)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならfalse
            if (_isWriteFailed) return false;

            //成功なら書き込み処理とtrue
            _gachaStoneAmount = dto.GachaStoneAmount;
            return true;
        }


        /// <summary>
        /// ランダムパック抽選結果のDTOを読み込む
        /// 読み込みに失敗した場合はnullが帰ってくる
        /// </summary>
        /// <returns>サーバのデータから作成したランダムパック抽選結果のDTO</returns>
        public async UniTask<RandomPackDTO> GetRandomPackDTO()
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            return new RandomPackDTO(_randomUserName, _randomCharacterType);
        }


        /// <summary>
        /// レコメンドパック抽選結果のDTOを読み込む
        /// 読み込みに失敗した場合はnullが帰ってくる
        /// </summary>
        /// <returns>サーバのデータから作成したレコメンドパック抽選結果のDTO</returns>
        public async UniTask<RecommendPackDTO> GetRecommendPackDTO()
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            return new RecommendPackDTO(_recommendUserName, _recommendCharacterType, _advice);
        }


        /// <summary>
        /// 会話相手一覧を読み込む
        /// 読み込みに失敗した場合はnullが帰ってくる
        /// </summary>
        /// <returns>会話相手の情報配列</returns>
        public async UniTask<ChatInfoDTO[]> GetChatInfoDTO()
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            var DTOArray = new ChatInfoDTO[_chatInfoArray.Length];
            for(int i = 0; i < DTOArray.Length; i++)
            {
                //ルームID発行
                _chatInfoArray[i].RoomId = Guid.NewGuid();
                var dto = new ChatInfoDTO(_chatInfoArray[i].RoomId, _chatInfoArray[i].UserName, _chatInfoArray[i].ImageURL);
                DTOArray[i] = dto;
            }
            return DTOArray;
        }


        public async UniTask<MessageInDTO[]> GetAllMessageInDTO(Guid roomID)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            int num= 0;
            for (int i = 0; i < _chatInfoArray.Length; i++)
            {
                if (_chatInfoArray[i].RoomId == roomID)
                {
                    num = i;
                    break;
                }
            }

            return _messageArray[num];
        }


        public async UniTask<MessageInDTO> GetMessageInDTO(Guid roomID)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            int num = 0;
            for (int i = 0; i < _chatInfoArray.Length; i++)
            {
                if (_chatInfoArray[i].RoomId == roomID)
                {
                    num = i;
                    break;
                }
            }

            //WebSocket用、今はとりあえず一個適当に返す
            return new MessageInDTO(Guid.NewGuid(), "サーバからの新しいメッセージ", $"https{num+1}", false, _chatInfoArray[num].UserName);
        }


        public async UniTask<bool> SetMessageOutDTO(MessageOutDTO dto)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならfalse
            if (_isWriteFailed) return false;

            //成功なら書き込み処理とtrue
            Debug.Log($"<color=purple>サーバがメッセージを受け取りました</color>：{dto.RoomID.ToString()}, {dto.Message}");
            return true;
        }





        /// <summary>
        /// トピック一覧を読み込む
        /// 読み込みに失敗した場合はnullが帰ってくる
        /// </summary>
        /// <returns>トピックの情報配列</returns>
        public async UniTask<ForumDTO[]> GetForumDTO()
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            var DTOArray = new ForumDTO[_topicInfoArray.Length];
            for (int i = 0; i < DTOArray.Length; i++)
            {
                //ルームID発行
                _topicInfoArray[i].RoomId = Guid.NewGuid();
                var dto = new ForumDTO(_topicInfoArray[i].RoomId, _topicInfoArray[i].UserName, _topicInfoArray[i].ImageURL, _topicInfoArray[i].Title);
                DTOArray[i] = dto;
            }
            return DTOArray;
        }


        public async UniTask<MessageInDTO[]> GetAllMessageInDTOForForum(Guid roomID)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            int num = 0;
            for (int i = 0; i < _topicInfoArray.Length; i++)
            {
                if (_topicInfoArray[i].RoomId == roomID)
                {
                    num = i;
                    break;
                }
            }

            Debug.Log($"{_topicArray[num] == null}");

            return _topicArray[num];
        }


        public async UniTask<MessageInDTO> GetMessageInDTOForForum(Guid roomID)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならnull
            if (_isReadFailed) return null;

            //成功なら適切なDTO
            int num = 0;
            for (int i = 0; i < _topicInfoArray.Length; i++)
            {
                if (_topicInfoArray[i].RoomId == roomID)
                {
                    num = i;
                    break;
                }
            }

            //WebSocket用、今はとりあえず一個適当に返す
            return new MessageInDTO(Guid.NewGuid(), "サーバからの新しい投稿", $"https{num + 1}", false, "Test");
        }


        public async UniTask<bool> SetMessageOutDTOForForum(MessageOutDTO dto)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(_executionTime);

            //失敗ならfalse
            if (_isWriteFailed) return false;

            //成功なら書き込み処理とtrue
            Debug.Log($"<color=purple>サーバが投稿を受け取りました</color>：{dto.RoomID.ToString()}, {dto.Message}");
            return true;
        }




        [Serializable] private struct ChatInfo
        {
            public Guid RoomId;
            public string UserName;
            public string ImageURL;
        }


        [Serializable] private struct ForumInfo
        {
            public Guid RoomId;
            public string UserName;
            public string ImageURL;
            public string Title;
        }
    }
}
