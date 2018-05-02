using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EmitMapper;
using TMall.Framework.Mapping.Configuration;
using System.Data;
using System.Diagnostics;
using TMall.Framework.Mapping.MapAttribute;
using AutoMapper;

namespace TMall.Framework.Mapping
{
    public class TestCustomMappingConfigObj
    {
        public string field1;
        public int field2;
        public bool field3;
    }

    public class TestCustomMappingConfig
    {
        [Test]
        public void TestCustomMapping()
        {
            var store = new Dictionary<string, object>();
            var mapperStore2Object =
                ObjectMapperManager
                .DefaultInstance
                .GetMapper<Dictionary<string, object>, TestCustomMappingConfigObj>(
                    new Store2ObjectMappingConfig()
                );
            var mapperObject2Store =
                ObjectMapperManager
                .DefaultInstance
                .GetMapper<TestCustomMappingConfigObj, Dictionary<string, object>>(
                    new Object2StoreMappingConfig()
                );

            store["field1"] = "test1";
            store["field2"] = 1;
            store["field3"] = true;

            TestCustomMappingConfigObj obj1 = mapperStore2Object.Map(store);
            Assert.AreEqual("test1", obj1.field1);
            Assert.AreEqual(1, obj1.field2);
            Assert.AreEqual(true, obj1.field3);

            obj1.field1 = "test2";
            obj1.field2 = 2;
            obj1.field3 = false;
            store.Clear();
            mapperObject2Store.Map(obj1, store);

            Assert.AreEqual("test2", store["field1"]);
            Assert.AreEqual(2, store["field2"]);
            Assert.AreEqual(false, store["field3"]);
        }

        [Test]
        public void AnonymousClassMapingTest()
        {
            var mapper =
               ObjectMapperManager
               .DefaultInstance
               .GetMapper<dynamic, TestObj>(
                   new AnonymousClassMapingConfig()
               );

            dynamic dytmp = new
            {
                field1 = "test2",
                field2 = 2,
                field3 = true,
                Students = new List<Student>() {
                     new Student(){ StudentName="StudentName1", StudentAge=21 , StudentAddress="StudentAddress1"},
                     new Student(){ StudentName="StudentName2", StudentAge=22 , StudentAddress="StudentAddress2"},
                     new Student(){ StudentName="StudentName3", StudentAge=23 , StudentAddress="StudentAddress3"}
                }
            };

            TestObj obj1 = mapper.Map(dytmp);
        }
    }

    public class TestObj
    {
        public string field1;
        public int field2;
        public bool field3;

        public List<Student> Students;
    }

    public class PersonDTO
    {
        [EDataMapping(MapName = "PersonName", MapType = DbType.String)]
        public string Name { get; set; }

        public int Age { get; set; }

        [EIgnore]
        public string Address { get; set; }

        public decimal Amount { get; set; }

        public List<Student> Students { get; set; }
    }

    public class Person
    {
        public string PersonName { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public decimal Amount { get; set; }

        public List<Student> Students { get; set; }
    }

    public class Student
    {
        public string StudentName { get; set; }

        public int StudentAge { get; set; }

        public string StudentAddress { get; set; }
    }

    [TestFixture]
    public class CommonMappingTest
    {
        [Test]
        public void Test()
        {
            //PersonDTO dto = new PersonDTO() { Name = "tiger", Age = 10, Amount = 88.888M, Address = "湖北武汉" };
            //var mapper = ObjectMapperManager.DefaultInstance
            //                    .GetMapper<PersonDTO, Person>(new CommonMapingConfig());

            //Person p = mapper.Map(dto);

            //Person person = new Person() { PersonName = "tiger", Age = 10, Amount = 88.888M, Address = "湖北武汉" };

            //var mapper2 = ObjectMapperManager.DefaultInstance
            //                  .GetMapper<Person, PersonDTO>(new CommonMapingConfig());

            //PersonDTO pdto = mapper2.Map(person);

            var mapper3 = ObjectMapperManager.DefaultInstance
                              .GetMapper<dynamic, Person>(new CommonMapingConfig());

            dynamic person2 = new { PersonName = "tiger", Age = 10, Amount = 88.888M, Address = "湖北武汉" };

            Person p2 = mapper3.Map(person2);
        }

        [Test]
        public static void DynamicTest()
        {
            var personDTO = new
            {
                Name = "tiger",
                Age = 10,
                Amount = 88.888M,
                Address = "湖北武汉",
                Students = new List<Student>() {
                     new Student(){ StudentName="StudentName1", StudentAge=21 , StudentAddress="StudentAddress1"},
                     new Student(){ StudentName="StudentName2", StudentAge=22 , StudentAddress="StudentAddress2"},
                     new Student(){ StudentName="StudentName3", StudentAge=23 , StudentAddress="StudentAddress3"}
                }
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();
            //var Person = MapperHelper.DynamicToEntity<Person>(personDTO);
            var Person = Mapper.DynamicMap<Person>(personDTO);
            sw.Stop();
            Console.WriteLine("emitMapper for " + ":" + sw.Elapsed);
        }

         [Test]
        public static void CommonMappingVSAutoMapper()
        {
            int mappingsCount = 1;
            var sw = new Stopwatch();

            PersonDTO s = new PersonDTO()
            {
                Name = "tiger",
                Age = 10,
                Amount = 88.888M,
                Address = "湖北武汉",
                Students = new List<Student>() {
                     new Student(){ StudentName="StudentName1", StudentAge=21 , StudentAddress="StudentAddress1"},
                     new Student(){ StudentName="StudentName2", StudentAge=22 , StudentAddress="StudentAddress2"},
                     new Student(){ StudentName="StudentName3", StudentAge=23 , StudentAddress="StudentAddress3"}
                }
            };

            //var sourceMember = s.GetType().GetProperty("Name");
            //sw.Start();
            //for (int i = 0; i < mappingsCount; ++i)
            //{
            // var customAttrs = sourceMember.GetCustomAttributes(typeof(EDataMappingAttribute), false);
            // var memberName = customAttrs != null && customAttrs.Length > 0 ? (customAttrs[0] as EDataMappingAttribute).MapName
            // : sourceMember.Name;
            //}
            //sw.Stop();
            //Console.WriteLine("GetProperty for " + mappingsCount + ":" + sw.Elapsed);

            Person d = new Person();

            //var mapper2 = ObjectMapperManager.DefaultInstance
            // .GetMapper<PersonDTO, Person>();

            var mapper = ObjectMapperManager.DefaultInstance
            .GetMapper<PersonDTO, Person>(new CommonMapingConfig());

            sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < mappingsCount; ++i)
            {
                d = mapper.Map(s);
            }
            sw.Stop();
            Console.WriteLine("emitMapper for " + mappingsCount + ":" + sw.Elapsed);

            AutoMapper.Mapper.CreateMap<PersonDTO, Person>();
            sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < mappingsCount; ++i)
            {
                d = AutoMapper.Mapper.Map<PersonDTO, Person>(s);
                //d = mapper2.Map(s);
            }
            sw.Stop();
            Console.WriteLine("autoMapper for " + mappingsCount + ":" + sw.Elapsed);
        }
    }
}
    
