using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectTracker
{
    public class ProjectsAccess
    {
        private List<ResearchProject> Projects;
        private readonly string _storageLocation = System.AppDomain.CurrentDomain.BaseDirectory + "storage.json";
        private FileStream storageStream;

        public ProjectsAccess()
        {
            storageStream = null;
            FileInfo storageFile = new FileInfo(@_storageLocation);

            WaitingForAccessWindow wait = new WaitingForAccessWindow();
            wait.Show();
            while (IsStorageInUse(storageFile))
            {
                //try every 10 seconds
                Thread.Sleep(5000);
            }
            try
            {
                storageStream = storageFile.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {

                throw;
            } 
            using (StreamReader reader = new StreamReader(storageStream))
            {
                Projects = JsonConvert.DeserializeObject<List<ResearchProject>>(reader.ReadToEnd());
            }
            if (Projects == null)
            {
                Projects = new List<ResearchProject>();
            }
            wait.Close();
            storageStream = storageFile.Open(FileMode.Create, FileAccess.Write, FileShare.None);
        }

        internal bool IsStorageInUse(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

        // Application calls this to get updated list (after editing, adding, removing etc.- use a PropertyChanged event or something).
        // May also want to create an overload that takes a search profile (to use for LINQ)- depends on customer need 
        internal List<ResearchProject> GetProjectsList()
        {
            return Projects.OrderBy(x => x.AssociatedFaculty).ToList();
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
                    storageStream.Dispose();
                    throw;
                }
                
            }
        }

        internal void UpdateProject(ResearchProject proj)
        {
            try
            {
                Projects.Remove(Projects.Single(x => x.AssociatedFaculty == proj.AssociatedFaculty && x.LastModified == proj.LastModified));
                proj.LastModified = DateTime.Now;
                AddProject(proj);
            }
            catch (Exception)
            {
                storageStream.Dispose();
                throw;
            }
        }

        internal void RemoveProject(ResearchProject proj)
        {
            try
            {
                ResearchProject toRemove = Projects.Single(x => x.Equals(proj));
                Projects.Remove(toRemove);
            }
            catch (Exception)
            {
                storageStream.Dispose();
                throw;
            }
        }

        internal void RemoveAllProjects()
        {
            //need to have a password of some kind (??)
            Projects = new List<ResearchProject>();
        }

        internal void CloseStorage()
        {
            //if stream can't be written it has closed already
            if (storageStream.CanWrite)
            {
                string json = JsonConvert.SerializeObject(Projects);
                try
                {
                    using (StreamWriter writer = new StreamWriter(storageStream))
                    {
                        writer.Write(json);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    storageStream.Dispose();
                }
            }
        }
    }
}
