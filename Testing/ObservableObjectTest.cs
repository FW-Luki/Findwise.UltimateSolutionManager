using System;
using System.Collections.ObjectModel;
using Findwise.Sharepoint.SolutionInstaller.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class ObservableObjectTest
    {
        private class TestObject : ObservableObject
        {
            public object AnyTypeProperty { get => Property<object>(); set => Property(value); }

            public double MagicNumber { get => Property<double>(); set => Property(value); }

            [DependantOn(nameof(MagicNumber))]
            public string DependantProperty { get => "Magic number is " + MagicNumber; }

            public ObservableCollection<object> Collection { get => Property(() => new ObservableCollection<object>()); }
        }

        [TestMethod]
        public void PropertyOfAnyTypeSetTest()
        {
            bool pass = false;
            var obj = new TestObject();
            obj.PropertyChanged += (sender, e) => { if (e.PropertyName == nameof(TestObject.AnyTypeProperty)) pass = true; };
            obj.AnyTypeProperty = new object();
            if (!pass) Assert.Fail($"Event not raised for property {nameof(TestObject.AnyTypeProperty)}");
        }

        [TestMethod]
        public void DependantPropertyTest()
        {
            bool masterPass = false, slavePass = false;
            var obj = new TestObject();
            obj.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(TestObject.MagicNumber)) masterPass = true;
                if (e.PropertyName == nameof(TestObject.DependantProperty)) slavePass = true;
            };
            obj.MagicNumber = 1500.2900;
            if (!masterPass) Assert.Fail($"Event not raised for property {nameof(TestObject.MagicNumber)}");
            if (!slavePass) Assert.Fail($"Event not raised for property {nameof(TestObject.DependantProperty)}");
        }

        [TestMethod]
        public void CollectionChangedTest()
        {
            bool pass = false;
            var obj = new TestObject();            
            obj.PropertyChanged += (sender, e) => { if (e.PropertyName == nameof(TestObject.Collection)) pass = true; };
            obj.Collection.Add(new object());
            if (!pass) Assert.Fail($"Event not raised for property {nameof(TestObject.Collection)}");
        }

        [TestMethod]
        public void CollectionItemChangedTest()
        {
            bool pass = false;
            var obj = new TestObject();
            var sub = new TestObject();           
            obj.Collection.Add(sub);
            obj.PropertyChanged += (sender, e) => { if (e.PropertyName == nameof(TestObject.Collection)) pass = true; };
            sub.AnyTypeProperty = new object();
            if (!pass) Assert.Fail($"Event not raised for property {nameof(TestObject.Collection)}");
        }

    }
}
