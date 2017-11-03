using System;
using System.Web.Mvc;
using Ixq.Core.Repository;
using Ixq.Demo.Entities;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.IO;
using System.Threading;
using System.Web;
using Autofac.Features.Indexed;
using Autofac.Integration.Mvc;
using Ixq.Demo.Web.Models;
using Ixq.DependencyInjection.Autofac.Extensions;
using Ixq.Core.DependencyInjection.Extensions;

namespace Ixq.Demo.Web.Controllers
{
    public class TestController : BaseController
    {
        private readonly IRepository<ProductType> _productTypeRepository;

        public TestController(IRepository<ProductType> productTypeRepository,
            IIndex<string,Models.IAnimal> index)
        {
            _productTypeRepository = productTypeRepository;
        }

        // GET: Test
        public ActionResult Index()
        {
            var bird = base.ServiceProvider.GetService<IAnimal>();
            var bird1 = base.ServiceProvider.GetService<IAnimal>("bird");
            var bird2 = Ixq.Core.DependencyInjection.ServiceProvider.Current.GetService<IAnimal>("bird");

            var tiger = ServiceProvider.GetService<IAnimal>("Tiger");
            var tiberObj = ServiceProvider.GetService(typeof(IAnimal), "Tiger");
            var tigerObj1 = Ixq.Core.DependencyInjection.ServiceProvider.Current.GetService(typeof(IAnimal), "Tiger");

            var pt = _productTypeRepository.SqlQuerySingle(Guid.Parse("2FE82B0D-C421-484B-A9CC-8D6E4520EBBC"));
            return View();
        }

        public ActionResult Exception()
        {
            throw new Exception("Exp");
            return View();
        }

        public ActionResult Youtube(string id = "ThIBNB5kUsU")
        {
            var res = getVideoInfo(id);
            return View();
        }


        private static List<Hashtable> getVideoInfo(string vid)
        {
            List<Hashtable> infoList = new List<Hashtable>();//信息列表
            //下载信息文件
            string infoURL = "http://www.youtube.com/get_video_info?video_id=" + vid;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(infoURL);
            Stream ns = request.GetResponse().GetResponseStream();
            byte[] nbytes = new byte[512];
            int nReadSize;
            string info = "";
            while (true)
            {
                nReadSize = ns.Read(nbytes, 0, 512);
                if (nReadSize == 0) break;
                //转成字符串
                info += Encoding.Default.GetString(nbytes);
            }
            //提取出视频关键信息部分
            string regStr = "&url_encoded_fmt_stream_map=(.+?)&";
            Regex reg = new Regex(regStr);
            Match match = reg.Match(info);
            info = match.Groups[1].Value;
            //解码
            info = Uri.UnescapeDataString(info);

            string[] parts = info.Split(',').ToArray();//每种清晰度的参数
            Hashtable ht;//存放一种清晰度的参数键值对
            for (int i = 0; i < parts.Length; i++)
            {//对于每一种清晰度
                string[] part_info = parts[i].Split('&').ToArray();//一种清晰度的参数
                ht = new Hashtable();//新建一个HashTable
                for (int j = 0; j < part_info.Length; j++)
                {
                    string[] pair = part_info[j].Split('=').ToArray();//键值对,itag,quality, type ,fallback_host,url
                    ht.Add(pair[0], Uri.UnescapeDataString(pair[1]));
                }
                //加入列表
                infoList.Add(ht);
            }

            return infoList;
        }
    }
}