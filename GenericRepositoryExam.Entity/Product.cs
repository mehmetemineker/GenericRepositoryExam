using GenericRepositoryExam.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericRepositoryExam.Entity
{
    public class Product : IEntity
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}
