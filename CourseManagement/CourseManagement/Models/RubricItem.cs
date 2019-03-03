using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.Models
{
    public class RubricItem
    {
        /// <summary>Gets the CRN.</summary>
        /// <value>The CRN.</value>
        public int CRN { get; set; }
        public string AssignmentType { get; set; }
        public int AssignmentWeight { get; set; }
        public int Index { get; set; }
        public RubricItem(int CRN, string assignmentType, int assignmentWeight, int index)
        {
            this.CRN = CRN;
            this.AssignmentType = assignmentType;
            this.AssignmentWeight = assignmentWeight;
            this.Index = index;
        }
    }
}