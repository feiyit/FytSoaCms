﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FytSoa.Web
{
    public class PageHelper
    {
        public static string HtmlsBBs(int page, int pagesize, long total, string url)
        {
            var str = "";
            if (total<2)
            {
                return str;
            }
            if (page!=1)
            {
                str += "<li><a class=\"prev page-numbers\" href=\""+url+"\">&lt;</a></li>";
            }
            
            int maxi = Convert.ToInt32(total);
            if (maxi < 10)
            {
                for (int i = 1; i < maxi + 1; i++)
                {
                    if (i == page)
                    {
                        str += "<li><span class=\"page-numbers current\">" + i + "</span></li>";
                    }
                    else
                    {
                        str += "<li><a class=\"page-numbers\" href=\"" + url + "?page=" + i + "\">" + i + "</a></li>";
                    }
                }
            }
            else
            {
                int maxfeye = page + 5;
                int minfeye = page - 4;
                if (page < 6)
                {
                    minfeye = 1;
                    maxfeye = 11;
                }
                if (maxfeye > maxi)
                {
                    maxfeye = maxi;
                }
                for (int f = minfeye; f < maxfeye + 1; f++)//每页显示9个分页数字
                {
                    if (f == page)
                    {
                        str += "<li><span class=\"page-numbers current\">" + f + "</span></li>";
                    }
                    else
                    {
                        str += "<li><a class=\"page-numbers\" href=\"" + url + "?page=" + f + "\">" + f + "</a></li>";

                    }
                }
            }

            if (page!=total)
            {
                str += "<li><a class=\"next page-numbers\" href=\""+url+"+?page="+(page+1) +"\">&gt;</a></li>";
            }
            return str;
        }

        public static string Htmls(int page, int pagesize, int total, string url)
        {
            var str = "";
            int maxi = total;
            if (maxi < 10)
            {
                for (int i = 1; i < maxi + 1; i++)
                {
                    if (i == page)
                    {
                        str+= "<a class=\"active\">" + i + "</a>";
                    }
                    else
                    {
                        str += "<a href=\"" + url + "?page=" + i + "\">" + i + "</a>";
                    }
                }
            }
            else
            {
                int maxfeye = page + 5;
                int minfeye = page - 4;
                if (page < 6)
                {
                    minfeye = 1;
                    maxfeye = 11;
                }
                if (maxfeye > maxi)
                {
                    maxfeye = maxi;
                }
                for (int f = minfeye; f < maxfeye + 1; f++)//每页显示9个分页数字
                {
                    if (f == page)
                    {
                        str += "<a class=\"active\">" + f + "</a>";
                    }
                    else
                    {
                        str += "<a href=\"" + url + "?page=" + f + "\">" + f + "</a>";

                    }
                }


            }
           
            /*var str = "<a href=\"" + url + "\">首页</a>";
            if (page > 1)
            {
                str += "<a href=\"" + url + "?page=" + (page - 1) + "\">上一页</a>";
            }
            else
            {
                str += "<a href=\"" + url + "\">上一页</a>";
            }
            if (page == total)
            {
                str += "<a href=\"javascript:void(0)\" disabled=\"disabled\">下一页</a>";
            }
            else
            {
                str += "<a href=\"" + url + "?page=" + (page + 1) + "\">下一页</a>";
            }
            if (total > 1)
            {
                str += "<a href=\"" + url + "?page=" + total + "\">尾页</a>";
            }
            else
            {
                str += "<a href=\"" + url + "\">尾页</a>";
            }*/
            return str;
        }

        /// <summary>
        /// 判断是否手机端浏览
        /// </summary>
        /// <returns></returns>
        //public bool IsMobile()
        //{
        //    string u = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
        //    Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        //    Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        //    if ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

    }
}