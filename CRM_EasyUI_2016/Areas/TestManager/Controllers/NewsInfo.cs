using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM_EasyUI_2016.Areas.TestManager.Controllers
{
    public class NewsInfo
    {
        public NewsInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int Num { get; set; }
        public string Ntitle { get; set; }

        /// <summary>
        /// 根据页码数获取数据
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static List<NewsInfo> GetListByPn(int pn)
        {
            List<NewsInfo> NewList = new List<NewsInfo>();
            NewList.Add(new NewsInfo { Num = 0, Ntitle = "华米科技宣布3500万美元B轮融资 估值超3亿美元" });
            NewList.Add(new NewsInfo { Num = 1, Ntitle = "淘汰CAPTCHA！谷歌推改良版CAPTCHA验证" });
            NewList.Add(new NewsInfo { Num = 2, Ntitle = "朋友圈做微商为何会如此遭人恨？买假货 还刷屏" });
            NewList.Add(new NewsInfo { Num = 3, Ntitle = "社交化新闻聚合网站的未来发展趋势" });
            NewList.Add(new NewsInfo { Num = 4, Ntitle = "雷军未来3~5年间将砸10亿美元投云计算" });
            NewList.Add(new NewsInfo { Num = 5, Ntitle = "Oculus CEO：我是如何邂逅扎克伯格的" });
            NewList.Add(new NewsInfo { Num = 6, Ntitle = "实战：股权众筹行业融资流程介绍" });
            NewList.Add(new NewsInfo { Num = 7, Ntitle = "理财范应邀加入中关村互联网金融行业协会" });
            NewList.Add(new NewsInfo { Num = 8, Ntitle = "P2P平台的“羊毛”还能继续撸吗？沉迷易受伤" });
            NewList.Add(new NewsInfo { Num = 9, Ntitle = "美副国务卿：美中都是网络攻击的受害者" });
            NewList.Add(new NewsInfo { Num = 10, Ntitle = "谷歌将推儿童版YouTube和Chrome浏览器" });
            NewList.Add(new NewsInfo { Num = 11, Ntitle = "高盛“免费”为Uber打车融资数亿美元" });
            NewList.Add(new NewsInfo { Num = 12, Ntitle = "观察:支付宝A股挂牌还需迈过几道槛" });
            NewList.Add(new NewsInfo { Num = 13, Ntitle = "优酷土豆刘德乐：多屏合一延伸视听产业新边界" });
            NewList.Add(new NewsInfo { Num = 14, Ntitle = "高盛“免费”为Uber打车融资数亿美元" });
            NewList.Add(new NewsInfo { Num = 15, Ntitle = "趣分期获1亿美金C轮融资 发力白领人群" });
            NewList.Add(new NewsInfo { Num = 16, Ntitle = "优酷土豆刘德乐：多屏合一延伸视听产业新边界" });
            NewList.Add(new NewsInfo { Num = 17, Ntitle = "社交化新闻聚合网站的未来发展趋势" });
            NewList.Add(new NewsInfo { Num = 18, Ntitle = "天天网董事长鞠传国：美妆平台还有上市空间" });
            NewList.Add(new NewsInfo { Num = 19, Ntitle = "百车宝 徐小平汽车领域投资第一单" });
            NewList.Add(new NewsInfo { Num = 20, Ntitle = "美副国务卿：美中都是网络攻击的受害者" });
            NewList.Add(new NewsInfo { Num = 21, Ntitle = "视频网站继续发力硬件 盒子依然是香饽饽" });
            NewList.Add(new NewsInfo { Num = 22, Ntitle = "谷歌推出网络机器人识别工具reCaptchas" });
            NewList.Add(new NewsInfo { Num = 23, Ntitle = "理财范应邀加入中关村互联网金融行业协会" });
            NewList.Add(new NewsInfo { Num = 24, Ntitle = "《江南Style》视频播放量爆表：谷歌被迫升级" });
            NewList.Add(new NewsInfo { Num = 25, Ntitle = "观察:支付宝A股挂牌还需迈过几道槛" });
            NewList.Add(new NewsInfo { Num = 26, Ntitle = "陌陌下周赴美上市 傍上阿里巴巴逆袭微信" });
            NewList.Add(new NewsInfo { Num = 27, Ntitle = "途牛同程封杀战升级：驴妈妈半路联手途牛" });
            NewList.Add(new NewsInfo { Num = 28, Ntitle = "互联网时代更要尊重原创和梦想" });
            NewList.Add(new NewsInfo { Num = 29, Ntitle = "Skype前员工推出移动即时通讯应用Wire" });
            NewList.Add(new NewsInfo { Num = 30, Ntitle = "盘点：2014年Q3美国主要互联网企业财报汇总" });
            NewList.Add(new NewsInfo { Num = 31, Ntitle = "盘点：西方社交媒体与社会资本研究综述" });
            NewList.Add(new NewsInfo { Num = 32, Ntitle = "陌陌将在IPO同时向阿里巴巴与58同城增发新股" });
            NewList.Add(new NewsInfo { Num = 33, Ntitle = "从O2O闭环到推广通 大众点评移动广告创新不断" });
            NewList.Add(new NewsInfo { Num = 34, Ntitle = "佛山豪车相撞 玛莎拉蒂冲上花基保时捷" });
            NewList.Add(new NewsInfo { Num = 35, Ntitle = "一汽马自达高效保养服务提升品牌价值" });
            NewList.Add(new NewsInfo { Num = 36, Ntitle = "一汽大众速腾后悬架断裂事件持续 案例信息采集中" });
            NewList.Add(new NewsInfo { Num = 37, Ntitle = "居民自发组织“车管会” 保障权益化解停车难" });
            NewList.Add(new NewsInfo { Num = 38, Ntitle = "新能源车：强化充电设施准入门槛" });
            NewList.Add(new NewsInfo { Num = 39, Ntitle = "胡润豪车报告引争议 中国汽车文化尚未成熟" });
            NewList.Add(new NewsInfo { Num = 40, Ntitle = "725名速腾车主起诉一汽大众 厂家举行袖珍沟通会" });
            NewList.Add(new NewsInfo { Num = 41, Ntitle = "特斯拉PK比亚迪 谁是新能源车大赢家？" });
            NewList.Add(new NewsInfo { Num = 42, Ntitle = "深圳本田飞度享0.3万优惠送5000大礼包" });
            NewList.Add(new NewsInfo { Num = 43, Ntitle = "国家放开电动车资质：谁将站上“风口”" });
            NewList.Add(new NewsInfo { Num = 44, Ntitle = "特斯拉能否打破中国式电动车发展困境？" });
            NewList.Add(new NewsInfo { Num = 45, Ntitle = "人民日报各抒己见：插电车为何不插电" });
            NewList.Add(new NewsInfo { Num = 46, Ntitle = "评论：“停车场乱象”再证多头管理之弊" });
            NewList.Add(new NewsInfo { Num = 47, Ntitle = "时事图说：停车费给了谁" });
            NewList.Add(new NewsInfo { Num = 48, Ntitle = "评论：停车收费之乱不仅在于去向成谜" });
            NewList.Add(new NewsInfo { Num = 49, Ntitle = "评论：“巨额停车费”到底去哪儿了？" });
            NewList.Add(new NewsInfo { Num = 50, Ntitle = "一汽轿车召回部分奔腾B50轿车" });
            NewList.Add(new NewsInfo { Num = 51, Ntitle = "我国进口车月均超11万辆 SUV是绝对主力车型" });
            NewList.Add(new NewsInfo { Num = 52, Ntitle = "MPV 50%增速抢眼 家用化趋势拉动商用车企跨界" });
            NewList.Add(new NewsInfo { Num = 53, Ntitle = "别克将推全新敞篷车型 或命名\"Velite\"" });
            NewList.Add(new NewsInfo { Num = 54, Ntitle = "[深圳]本田锋范综合优惠2.6万元现车充足" });
            NewList.Add(new NewsInfo { Num = 55, Ntitle = "业内人士：汽车电商不会牺牲经销商利益" });
            NewList.Add(new NewsInfo { Num = 56, Ntitle = "11月经销商库存指数再高企" });
            NewList.Add(new NewsInfo { Num = 57, Ntitle = "整车企业牵手租车公司 全产业链合作挖掘消费增长.." });
            NewList.Add(new NewsInfo { Num = 58, Ntitle = "用车小贴士：延长爱车寿命10妙招" });
            NewList.Add(new NewsInfo { Num = 59, Ntitle = "温暖冬日 关怀延续昌河汽车续温暖传奇" });
            NewList.Add(new NewsInfo { Num = 60, Ntitle = "业主与业委会为何“有仇”？法规监管存空白" });
            NewList.Add(new NewsInfo { Num = 61, Ntitle = "财苑访谈：降息利好房地产 一线城市房价仍然看涨" });
            NewList.Add(new NewsInfo { Num = 62, Ntitle = "王中丙在2014中国海洋经济博览会论坛上发表主旨.." });
            NewList.Add(new NewsInfo { Num = 63, Ntitle = "地板同质化需要业内企业共同作用" });
            NewList.Add(new NewsInfo { Num = 64, Ntitle = "房地产永久产权成为现实后的9大猜想，你懂的" });
            NewList.Add(new NewsInfo { Num = 65, Ntitle = "世茂媒体行：世茂是如何将擅长的别墅做到了极致" });
            NewList.Add(new NewsInfo { Num = 66, Ntitle = "评论：小蛮腰巨亏 买单的是你我" });
            NewList.Add(new NewsInfo { Num = 67, Ntitle = "“房屋永久产权“引发热议 “老房子“反而更卖座" });
            NewList.Add(new NewsInfo { Num = 68, Ntitle = "电器起火为何不能用水浇" });
            NewList.Add(new NewsInfo { Num = 69, Ntitle = "贾康：房地产税立法将迎实质性安排" });
            NewList.Add(new NewsInfo { Num = 70, Ntitle = "公交减车减趟 廓清谣言更要读懂民心【长城时评】" });
            NewList.Add(new NewsInfo { Num = 71, Ntitle = "评论：谁解“亮化工程画楼”的风情？" });

            //IEnumerable<NewsInfo> query = from n in NewList where (n.Num >= 10 * pn && n.Num < 10 * (pn + 1)) select n;
            List<NewsInfo> ListQuery = (from n in NewList where (n.Num >= 10 * pn && n.Num < 10 * (pn + 1)) select n).ToList();
            return ListQuery;
        }
    }
}