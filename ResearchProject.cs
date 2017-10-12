using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker
{
    class ResearchProject
    {
        public DateTime LastModified { get; set; }
        public string Investigators { get; set; }
        public string CoInvestigators { get; set; }
        public string ProjectTitle { get; set; }
        public string Objectives { get; set; }
        public string Methods { get; set; }
        public string Results { get; set; }
        public string Conclusions { get; set; }
        public string FutureStudies { get; set; }

        public ResearchProject() { }
    }
}
