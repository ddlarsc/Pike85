using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Model
{
    //<Content>
    //  <p class=\"mobile-photo\">
    //      <a href=\"http://4.bp.blogspot.com/-jYPWCWsfKyk/UWriQj6lRfI/AAAAAAAABXg/xdIVeqek10I/s1600/IMAG0040-733471.jpg\">
    //          <img src=\"http://4.bp.blogspot.com/-jYPWCWsfKyk/UWriQj6lRfI/AAAAAAAABXg/xdIVeqek10I/s320/IMAG0040-733471.jpg\"  border=\"0\" alt=\"\" id=\"BLOGGER_PHOTO_ID_5866750238750230002\" />
    //      </a>
    //  </p>
    //</Content>

    //<Content> 
    //  <br/>
    //  <br/>
    //  <div class=\"separator\" style=\"clear: both; text-align: center;\">
    //      <a href=\"https://lh3.googleusercontent.com/-HmClW7z4yjk/UVn_ubyY89I/AAAAAAAAAis/hNR7dVE90WE/s640/blogger-image--558122517.jpg\" imageanchor=\"1\"  style=\"margin-left: 1em; margin-right: 1em;\">
    //          <img border=\"0\" src=\"https://lh3.googleusercontent.com/-HmClW7z4yjk/UVn_ubyY89I/AAAAAAAAAis/hNR7dVE90WE/s640/blogger-image--558122517.jpg\" />
    //      </a>
    //  </div>
    //</Content>

    //<Content>
    //  <div class=\"separator\"  style=\"clear: both; text-align: center;\">Uha, sikke en dag, jeg vil sige det meget kort, stilhed før storm :-) Filip ved hvad jeg mener :-)</div>
    //      <div class=\"separator\"  style=\"clear: both; text-align: center;\">
    //  </div>
    //  <div class=\"separator\"  style=\"clear: both; text-align: center;\">Filip med en super Laks på 11,50 kg...
    //      <a href=\"http://4.bp.blogspot.com/-j3YPjbLsweQ/UX6qsLxRPbI/AAAAAAAAAj8/mvuoAVBtv6Y/s1600/DSC_6791.JPG\" imageanchor=\"1\"  style=\"margin-left: 1em; margin-right: 1em;\">
    //          <img border=\"0\" height=\"214\" src=\"http://4.bp.blogspot.com/-j3YPjbLsweQ/UX6qsLxRPbI/AAAAAAAAAj8/mvuoAVBtv6Y/s320/DSC_6791.JPG\" width=\"320\" />
    //      </a>
    //  </div>
    //  <br />
    //  <div class=\"separator\"  style=\"clear: both; text-align: center;\">TC som gast, 13 kg. rent...
    //      <a href=\"http://2.bp.blogspot.com/-3vYpFv6W_qA/UX6qz2c5tmI/AAAAAAAAAkE/e4-UROQWwFo/s1600/DSC_6777.JPG\" imageanchor=\"1\"  style=\"margin-left: 1em; margin-right: 1em;\">
    //          <img border=\"0\" height=\"214\" src=\"http://2.bp.blogspot.com/-3vYpFv6W_qA/UX6qz2c5tmI/AAAAAAAAAkE/e4-UROQWwFo/s320/DSC_6777.JPG\" width=\"320\" />
    //      </a>
    //  </div>
    //  <br />
    //</Content>

    [Serializable]
    [XmlRoot("Content")]
    public class BlogResultContentPicture
    {
        [XmlElement("p")]
        public P P { get; set; }

        [XmlElement("div")]
        public List<Div> Div { get; set; }
    }

    [Serializable]
    [XmlRoot("p")]
    public class P
    {
        [XmlAttribute("class")]
        public string Class { get; set; }

        [XmlAttribute("Style")]
        public string Style { get; set; }

        [XmlElement("a")]
        public A Href { get; set; }
    }

    [Serializable]
    [XmlRoot("div")]
    public class Div
    {
        [XmlAttribute("class")]
        public string Class { get; set; }

        [XmlAttribute("Style")]
        public string Style { get; set; }

        [XmlElement("a")]
        public A Href { get; set; }
    }

    [Serializable]
    [XmlRoot("a")]
    public class A
    {
        [XmlAttribute("href")]
        public string Href { get; set; }

        [XmlAttribute("imageanchor")]
        public string ImageAnchor { get; set; }

        [XmlAttribute("Style")]
        public string Style { get; set; }

        [XmlElement("img")]
        public Img Img { get; set; }
    }

    [Serializable]
    [XmlRoot("img")]
    public class Img
    {
        [XmlAttribute("src")]
        public string Src { get; set; }

        [XmlAttribute("border")]
        public string Border { get; set; }

        [XmlAttribute("alt")]
        public string Alt { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("style")]
        public Style Style { get; set; }

        public override string ToString()
        {
            return string.Format("<img src='{0}' border='{1}' alt='{2}' id='{3}' {4}/>", Src, Border, Alt ?? "Goto", Id, Style);
        }
    }

    [Serializable]
    [XmlRoot("style")]
    public class Style
    {
        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("hight")]
        public int Hight { get; set; }

        public override string ToString()
        {
            return string.Format("style='width:{0}; hight:{1}'", Width == 0 ? 50 : Width, Hight == 0 ? 50 : Hight);
        }
    }
}