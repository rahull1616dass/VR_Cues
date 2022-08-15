using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Extensions
{
    public static class StringExt
    {
        public static string adjustMandatoryText(this string text, bool isMandatory)
        {
            return isMandatory ? $"{text}*" : text;
        }
        
        public static Sprite createSpriteFromReferenceId(this string referenceId)
        {
            //Creates texture and loads byte array data to create image
            Texture2D texture2DImage = new Texture2D(100, 100);
            texture2DImage.LoadImage(File.ReadAllBytes($"{Application.streamingAssetsPath}/images/{referenceId}"));

            //Creates a new Sprite based on the Texture2D
            return Sprite.Create(texture2DImage,
                new Rect(0.0f, 0.0f, texture2DImage.width, texture2DImage.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public static void appendToOutputFile(this string text)
        {

        }
    }
}
