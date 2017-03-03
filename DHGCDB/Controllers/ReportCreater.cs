using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DHGCDB.Models;
using DHGCDB.ViewModels;
using System.Linq;
using DHGCDB.Templates;


namespace DHGCDB.Controllers
{
  public class ReportCreater
  {
    private static string evaluate(string query, Review review)
    {
      string result = "";
      Func<Review, string> queryFunc;

      if(QueryMappings.ReviewData.TryGetValue(query, out queryFunc))
        result = queryFunc(review);

      result = result.Replace("&", "&amp;");

      return result;
    }

    public static string Create(Review review)
    {
      var templateDir = System.Configuration.ConfigurationManager.AppSettings["DHGCDBTemplateDirectory"];
      var outputDir = System.Configuration.ConfigurationManager.AppSettings["DHGCDBOutputDirectory"];

      var source = String.Format(@"{0}\template.docx", templateDir);
      var docName = String.Format(@"report_{0}.docx", review.ID);
      var dest = String.Format(@"{0}\{1}", outputDir, docName);

      var replacements = new Dictionary<string, string>();
      var psi_replacements = new Dictionary<string, string>();

      foreach(var key in QueryMappings.ReviewData.Keys.Where(x => Regex.IsMatch(x, "^PSI"))) {
        var replacetext = @" \[% " + key + @" %\]";
        psi_replacements[replacetext] = evaluate(key, review);
      }

      foreach(var key in QueryMappings.ReviewData.Keys) {
        var replacetext = @"\[% " + key + @" %\]";
        replacements[replacetext] = evaluate(key, review);
      }

      System.IO.File.Copy(source, dest, true);
      var doc = WordprocessingDocument.Open(dest, true);
      string docText = null;
      using(var sr = new System.IO.StreamReader(doc.MainDocumentPart.GetStream())) {
        docText = sr.ReadToEnd();
      }

      foreach(var item in psi_replacements) {
        var regexText = new Regex(item.Key);
        docText = regexText.Replace(docText, item.Value);
      }

      foreach(var item in replacements) {
        var regexText = new Regex(item.Key);
        docText = regexText.Replace(docText, item.Value);
      }

      using(var sw = new System.IO.StreamWriter(doc.MainDocumentPart.GetStream(System.IO.FileMode.Create))) {
        sw.Write(docText);
      }

//      using(var connection = new SqlConnection(Properties.Settings.Default.dhgcdbConnectionString)) {
//        string query = @"
//SELECT
//	f.name,
//	f.descr,
//	fatr.perc
//FROM
//	fund_selection_choice fsc
//	JOIN person_attitude_to_risk_investment patr on fsc.person_id=patr.person_id
//	JOIN fund f on f.fundselection_id=fsc.fund_selection_id
//	JOIN fund_investments_atr_allocations fatr on fatr.fund_id=f.id and fatr.attitude_to_risk_id=patr.attitude_to_risk_id
//WHERE
//	fsc.inv_type=1
//	AND fsc.review_instance_id=" + _reportInstance + @"
//	AND fsc.person_id=" + _primaryPerson.Value;
//
//        using(var command = new SqlCommand(query, connection)) {
//          connection.Open();
//          using(var commandReader = command.ExecuteReader()) {
//            DataTable dataTable = new DataTable();
//            dataTable.Load(commandReader);
//
//            foreach(DataRow row in dataTable.Rows) {
//              var run = doc.MainDocumentPart.Document.Body.AppendChild(new Paragraph()).AppendChild(new Run());
//              run.AppendChild(new Text(row[2].ToString() + "% - "));
//              run.AppendChild(new Text(row[0].ToString() + ". "));
//              run.AppendChild(new Text(row[1].ToString()));
//            }
//          }
//        }
//      }

      doc.Close();

      return docName;
    }
  }
}