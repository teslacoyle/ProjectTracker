using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace ProjectTracker
{
    class ReportCreator
    {
        string projectFormatString = "Investigator(s): {0}\n" +
                                     "Co-Investigator(s): {1}\n" +
                                     "Project Title: {2}\n" +
                                     "Objectives: {3}\n" +
                                     "Methods: {4}\n" +
                                     "Results: {5}\n" +
                                     "Conclusions: {6}\n" +
                                     "Future Studies: {7}\n";



        internal void CreateReport(bool leaveOpen)
        {
            ProjectsAccess pa = new ProjectsAccess();
            List<ResearchProject> Projects = pa.GetProjectsList();

            Word._Application _app = new Word.Application();
            
            if(leaveOpen)
                _app.Visible = true;

            object missing = Type.Missing;
            Word.Document doc = _app.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            Word.Paragraph currentParagraph = doc.Paragraphs.Add(ref missing);
            currentParagraph.Range.Text = "Independent Investigator Profiles";
            object styleHeading = "Heading 1";
            currentParagraph.set_Style(ref styleHeading);
            currentParagraph.Range.InsertParagraphAfter();

            foreach(ResearchProject proj in Projects)
            {
                currentParagraph.SpaceBefore = 0f;
                currentParagraph.SpaceAfter = 0f;
                currentParagraph.Range.Text = String.Format(projectFormatString, proj.Investigators, proj.CoInvestigators, proj.ProjectTitle, proj.Objectives, proj.Methods, proj.Results, proj.Conclusions, proj.FutureStudies);
                currentParagraph.Range.InsertParagraphAfter();
            }

            if (!leaveOpen)
            {
                object file_name = AppDomain.CurrentDomain.BaseDirectory + "test.docx";
                object file_type = Word.WdSaveFormat.wdFormatDocumentDefault;
                doc.SaveAs(ref file_name, ref file_type, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                object save_changes = false;
                doc.Close(ref save_changes, ref missing, ref missing);
                _app.Quit(ref save_changes, ref missing, ref missing);
            }
            pa.CloseStorage();
        } 

    }
}
