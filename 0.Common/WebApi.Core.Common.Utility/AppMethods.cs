using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace WebApi.Core.Utility
{
    public class AppMethods
    {
        static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public static string EncryptPassword(string password)
        {
            //to do: encrypt the password using some hashing algorithm
            return password; 
        }

        public static string EncryptPassword(string inputPassword, string EncryptedPassword)
        {
            //to do : compare encrypted the password using some hashing algorithm

            return inputPassword;
        }

        public static string SerializeToXml<T>(T obj)
        {
            var result = string.Empty;
            try
            {
                var ser = new XmlSerializer(typeof(T));
                var writer = new StringWriter();
                ser.Serialize(writer, obj);
                result = writer.ToString();
                writer.Close();
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return result;
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            T result;
            var ser = new XmlSerializer(typeof(T));
            using (TextReader tr = new StringReader(xml))
            {
                result = (T)ser.Deserialize(tr);
            }
            return result;
        }

        public static Dictionary<int, string> GenderList()
        {
            var genderList = new Dictionary<int, string>();

            foreach (Gender e in Enum.GetValues(typeof(Gender)))
            {
                genderList.Add((int)e, Enum.GetName(typeof(Gender), e));
            }

            return genderList;
        }

        public static Dictionary<int, string> NationalityList()
        {
            var nationalityList = new Dictionary<int, string>();

            foreach (Nationality e in Enum.GetValues(typeof(Nationality)))
            {
                nationalityList.Add((int)e, Enum.GetName(typeof(Nationality), e));
            }

            return nationalityList;
        }

        public static string TransformXmlToHtml(string inputXml, string xsltFilePath)
        {
            var result = string.Empty;
            try
            {
                using (StringWriter stringWriter = new StringWriter())
                {
                    var xslCompiledTransform = new XslCompiledTransform();
                    var settings = new XsltSettings
                    {
                        EnableScript = true
                    };

                    xslCompiledTransform.Load(xsltFilePath, settings, null);

                    var doc = new XPathDocument(new StringReader(inputXml));

                    using (StringWriter sw = new StringWriter())
                    {
                        xslCompiledTransform.Transform(doc, null, sw);
                        result = sw.ToString();
                    }
                    return result;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public static string HtmlStringToPdfFile(string pdfOutputLocation, string outputFilename, string htmlData, string pdfHtmlToPdfExePath)
        {
            System.IO.StreamWriter stdin;
            try
            {
                //Determine inputs
                if (string.IsNullOrEmpty(htmlData))
                {
                    //log.Info("No input string is provided for HtmlToPdf.");
                    throw new Exception("No input string is provided for HtmlToPdf");
                }

               // string outputFolder = pdfOutputLocation;
                //log.Info("pdf Generation initiated.");

                var p = new System.Diagnostics.Process()
                {
                    StartInfo =
                    {
                        FileName = AppProperties.BasePhysicalPath + pdfHtmlToPdfExePath,
                        Arguments = "-q -n - " + outputFilename,
                        UseShellExecute = false, // needs to be false in order to redirect output
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true, // redirect all 3, as it should be all 3 or none
                        WorkingDirectory = AppProperties.BasePhysicalPath +pdfOutputLocation
                    }
                };
                p.Start();
                //log.Info("pdf Generation Started.");

                using (
                //log.Info("pdf Generation Started.");

                stdin = new StreamWriter(p.StandardInput.BaseStream, Encoding.UTF8))
                {
                    stdin.AutoFlush = true;
                    stdin.Write(htmlData);
                    stdin.Close();
                    //log.Info("pdf Generated.");

                    // read the output here...
                    var output = p.StandardOutput.ReadToEnd();
                    var errorOutput = p.StandardError.ReadToEnd();

                    // ...then wait n milliseconds for exit (as after exit, it can't read the output)
                    p.WaitForExit(60000);

                    // read the exit code, close process
                    int returnCode = p.ExitCode;
                    p.Close();
                    p.Dispose();
                    p.Refresh();
                    // if 0 or 2, it worked so return path of pdf
                    if ((returnCode == 0) || (returnCode == 2))
                        return outputFilename;
                    else
                        throw new Exception(errorOutput);
                }
            }
            catch (Exception exc)
            {
                //log.Info("Problem generating PDF from HTML string." + exc.Message);

                throw new Exception("Problem generating PDF from HTML string, outputFilename: " + outputFilename, exc);
            }
        }

        public static T GetCache<T>(string key) where T : class
        {
            if(cache.TryGetValue(key, out object result)) { 
                return (T)result;
            }
            else{
                return null;
            }
        }

        public static void AddCache(string key)
        {
            try
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1.0)
                };

                if (!cache.TryGetValue(key, out object result))
                {
                    cache.Set(key,result, cacheEntryOptions);
                }
            }
            catch
            {
                throw new Exception("AddCache failed");
            }
        }

        public static Guid GetGuid(string value)
        {
            var result = default(Guid);
            Guid.TryParse(value, out result);
            return result;
        }

        public static string GetHash(string input)
        {
            using (HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                var byteValue = System.Text.Encoding.UTF8.GetBytes(input);

                var byteHash = hashAlgorithm.ComputeHash(byteValue);

                return Convert.ToBase64String(byteHash);
            }
        }
    }
}
