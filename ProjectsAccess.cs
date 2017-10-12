using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker
{
    class ProjectsAccess
    {
        private List<ResearchProject> Projects;
        private readonly string _storageLocation = System.AppDomain.CurrentDomain.BaseDirectory + "test.json";        // Will eventually be the string of the JSON file where data is stored

        public ProjectsAccess()
        {
            if(File.Exists(@_storageLocation))
            {
                Projects = JsonConvert.DeserializeObject<List<ResearchProject>>(File.ReadAllText(@_storageLocation));
            }
            else
            {
                Projects = new List<ResearchProject>();
                //Remember to uncomment this, Eli.
                //throw new FileNotFoundException();
            }
        }

        // Application calls this to get updated list (after editing, adding, removing etc.- use a PropertyChanged event or something).
        // May also want to create an overload that takes a search profile (to use for LINQ)- depends on customer need 
        internal List<ResearchProject> GetProjectsList()
        {
            return Projects;
        }
        
        internal void AddProject(ResearchProject proj)
        {
            if(!Projects.Contains(proj))
            {
                try
                {
                    Projects.Add(proj);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }

        internal void RemoveProject(string projName)
        {
            try
            {
                ResearchProject toRemove = Projects.Single(x => x.ProjectTitle == projName);
                Projects.Remove(toRemove);
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void RemoveAllProjects()
        {
            //need to have a password of some kind (??)
            Projects = new List<ResearchProject>();
        }

        internal void OnClosing()
        {
            string json = JsonConvert.SerializeObject(Projects);
            try
            {
                // TODO write to _storageLocation
                File.WriteAllText(@_storageLocation, json);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
