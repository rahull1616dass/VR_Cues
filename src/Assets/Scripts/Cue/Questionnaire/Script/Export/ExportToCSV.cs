using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine.Events;
using System;

/// <summary>
/// ExportToCSV.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class ExportToCSV
    {
        public string FileName;
        public string Delimiter;
        public enum FileType
        {
            Csv,
            Txt
        }
        public FileType Filetype;

        [Header("Configure if you want to save the results to local storage:")]
        [Tooltip("Save results locally on this device.")]
        public bool SaveToLocal = true;
        public string StorePath;

        //[Header("Configure if you want to save the results to remote server:")]
        //public bool SaveToServer = false;
        //[Tooltip("The target URI to send the results to")]
        //public string TargetURI = "http://www.example-server.com/survey-results.php";

        private string _folderPath;
        private string _fileType;
        private string _questionnaireID;
        private static List<string[]> questionAnswersList = new List<string[]>();

        public UnityEvent QuestionnaireFinishedEvent;

        public static int QuestionIndex;

        // Use this for initialization

        public static void SaveDataWhileAnswering(string QuestionTag, string Question, string Answer, int Index)
        {
            //Debug.Log("Using Index " + Index);
            string[] questionAnswer = new string[4];
            questionAnswer[0] = QuestionTag;
            questionAnswer[1] = Question;
            questionAnswer[2] = Index.ToString();
            questionAnswer[3] = Answer;
            if (questionAnswersList.Count > Index)
            {
                questionAnswersList[Index] = questionAnswer;
            }
            else
            {
                //Debug.Log("Created Index " + Index);
                questionAnswersList.Add(questionAnswer);
            }
        }

        public static void Save(string _folderPath, string _questionnaireID, string FileName, string _fileType)
        {
            List<string[]> _csvRows = new List<string[]>();
            string[] csvTitleRow = new string[4];
            // create title rows
            csvTitleRow[0] = "QuestionTag";
            csvTitleRow[1] = "Question";
            csvTitleRow[2] = "QuestionID";
            csvTitleRow[3] = "Answer";
            _csvRows.Add(csvTitleRow);
            foreach (var data in questionAnswersList)
            {
                _csvRows.Add(data);
            }



            //-----Processing responses into the specified data format-----//

            string _completeFileName = "questionnaireID_" + _questionnaireID + "_" + FileName + "." + _fileType;
            string _path = _folderPath + _completeFileName;


            string[][] output = _csvRows.ToArray(); ;


            StringBuilder contentOfResult = new StringBuilder();

            for (int index = 0; index < output.GetLength(0); index++)
                contentOfResult.AppendLine(string.Join(";", output[index]));


            WriteToLocal(_path, contentOfResult);
            /* WRITING RESULTS TO LOCAL STORAGE */
            //if (SaveToLocal)
            //{
            //    WriteToLocal(_path, contentOfResult);
            //}

            /* SENDING RESULTS TO REMOTE SERVER */
            //if (SaveToServer)
            //{
            //    StartCoroutine(SendToServer(TargetURI, _completeFileName, contentOfResult.ToString()));
            //}

            /* CONSOLIDATING RESULTS */
            //if (_studySetup.AlsoConsolidateResults)
            //{
            //    StringBuilder content_all_results = GetConsolidatedContent(_path_allResults, output);

            //    if (SaveToLocal)
            //    {
            //        WriteToLocal(_path_allResults, content_all_results);
            //    }

            //    if (SaveToServer)
            //    {
            //        StartCoroutine(SendToServer(TargetURI, _completeFileName_allResults, content_all_results.ToString()));
            //    }
            //}

            //QuestionnaireFinishedEvent.Invoke(); //notify 
        }

        /// <summary>
        /// Consolidate all results to a StringBuilder, written to be directly written.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="newData"></param>
        /// <returns></returns>
        //StringBuilder GetConsolidatedContent(string filepath, string[][] newData)
        //{
        //    StringBuilder sb_all_content = new StringBuilder();

        //    string header =  ""; // header for this current participant

        //    try
        //    {
        //        if (!File.Exists(filepath))
        //        {
        //            sb_all_content.AppendLine(csvTitleRow[0] + Delimiter + csvTitleRow[1] + Delimiter + csvTitleRow[2] + Delimiter + header); // first row being the headers
        //            for (int row = 1; row < newData.GetLength(0); row++) // from the second row
        //            {
        //                sb_all_content.AppendLine(string.Join(Delimiter, newData[row]));
        //            }
        //        }
        //        else
        //        {
        //            StreamReader sr = new StreamReader(filepath);
        //            sb_all_content.AppendLine(sr.ReadLine() + Delimiter + header); // copy the first row in the existing file and add a header for the new data
        //            for (int row = 1; row < newData.GetLength(0); row++) // from the second row
        //            {
        //                sb_all_content.AppendLine(sr.ReadLine() + Delimiter + newData[row][3]); // copy old data and add new data
        //            }
        //            sr.Close();
        //        }
        //    }
        //    catch (IOException ex)
        //    {
        //        Debug.Log(ex.Message);
        //    }
        //    return sb_all_content;
        //}

        /// <summary>
        /// Write a StringBuilder to a local file.
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="content"></param>
        static void WriteToLocal(string localPath, StringBuilder content)
        {
            Debug.Log("Answers stored in path: " + localPath);
            try
            {
                StreamWriter outStream = File.CreateText(localPath);
                outStream.WriteLine(content);
                outStream.Close();
            }
            catch (IOException ex)
            {
                Debug.Log(ex.Message);
            }
        }

        /// <summary>
        /// Post data to a specific server location.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="filename"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        //IEnumerator SendToServer(string uri, string filename, string inputData)
        //{
        //    WWWForm form = new WWWForm();
        //    form.AddField("fileName", filename);
        //    form.AddField("inputData", inputData);

        //    using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        //    {
        //        yield return www.SendWebRequest();

        //        if (www.isHttpError || www.isNetworkError)
        //        {
        //            Debug.LogError(www.error + "\nPlease check the validity of the server URI.");
        //        }
        //        else
        //        {
        //            string responseText = www.downloadHandler.text;
        //            Debug.Log("Message from the server: " + responseText);
        //        }
        //    }
        //}

        /// <summary>
        /// Check if the provided server URI is valid.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        //IEnumerator CheckURIValidity(string uri)
        //{
        //    UnityWebRequest www = new UnityWebRequest(uri);
        //    yield return www.SendWebRequest();

        //    if (www.isHttpError || www.isNetworkError)
        //    {
        //        Debug.LogError(www.error + "\nPlease check the validity of the server URI.");
        //    }
        //}
    }
}

