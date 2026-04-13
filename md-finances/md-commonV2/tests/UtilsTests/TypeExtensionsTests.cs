using System.Collections.Generic;
using FluentAssertions;
using Moedelo.CommonV2.Extensions.System;
using NUnit.Framework;

namespace UtilsTests
{
    public class EmptyClass
    {
    }

    public class OneFieldClass
    {
        public int Id { get; set; }
    }

    public class OneFieldClassCompatible
    {
        public int Id { get; set; }
    }

    public class OneFieldClassNotCompatible
    {
        public long Id { get; set; }
    }

    public enum Enum1
    {
        V1 = 0,
        V2 = 1
    }

    public enum Enum2
    {
        V1 = 10,
        V2 = 11
    }

    public enum Enum1Clone
    {
        V1Clone = 0,
        V2Clone = 1
    }

    public enum Enum1Reordered
    {
        V1Clone = 1,
        V2Clone = 0
    }

    public class ComplexClass
    {
        public class Item
        {
            public enum ItemEnum
            {
                v1 = 0,
                v2 = 1
            }

            public int Id { get; set; }
            public ItemEnum Enum { get; set; }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Item> Items { get; set; }
    }

    public class ComplexClassCompatible
    {
        public class Item
        {
            public enum ItemEnum
            {
                v1 = 0,
                v2 = 1
            }

            public int Id { get; set; }
            public ItemEnum Enum { get; set; }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Item> Items { get; set; }
    }

    public class ComplexClassNotCompatible
    {
        public class Item
        {
            public enum ItemEnum
            {
                v1 = 0,
                v2 = 2 // несовместимый Enum
            }

            public int Id { get; set; }
            public ItemEnum Enum { get; set; }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Item> Items { get; set; }
    }

    [TestFixture]
    public class TypeExtensionsTests
    {
        [Test(Description = "Сравнение типа с самим собой")]
        public void IsCompatible_ReturnsTrue_ForTheSameType()
        {
            typeof(EmptyClass).IsCompatibleTo(typeof(EmptyClass)).Should().BeTrue();
        }

        [Test(Description = "Сравнение enums с совместимым набором полей")]
        public void IsCompatible_ReturnsTrue_ForCompatibleEnums()
        {
            typeof(Enum1).IsCompatibleTo(typeof(Enum1Clone)).Should().BeTrue();
        }

        [Test(Description = "Сравнение enums с совместимым набором полей но разным порядком")]
        public void IsCompatible_ReturnsTrue_ForCompatibleReorderedEnums()
        {
            typeof(Enum1).IsCompatibleTo(typeof(Enum1Reordered)).Should().BeTrue();
        }

        [Test(Description = "Сравнение enums с несовместимым набором полей")]
        public void IsCompatible_ReturnsFalse_ForNonCompatibleEnums()
        {
            typeof(Enum1).IsCompatibleTo(typeof(Enum2)).Should().BeFalse();
        }

        [Test(Description = "Сравнение enum с int")]
        public void IsCompatible_ReturnsTrue_IntVsEnum()
        {
            typeof(int).IsCompatibleTo(typeof(Enum2)).Should().BeTrue();
        }

        [Test(Description = "Сравнение int с enum")]
        public void IsCompatible_ReturnsTrue_EnumVsInt()
        {
            typeof(Enum1).IsCompatibleTo(typeof(int)).Should().BeTrue();
        }

        [Test(Description = "Сравнение int с uint")]
        public void IsCompatible_ReturnsFalse_IntVsUint()
        {
            typeof(int).IsCompatibleTo(typeof(uint)).Should().BeFalse();
        }

        [Test(Description = "Сравнение int с long")]
        public void IsCompatible_ReturnsFalse_IntVsLong()
        {
            typeof(int).IsCompatibleTo(typeof(long)).Should().BeFalse();
        }

        [Test(Description = "Сравнение int со string")]
        public void IsCompatible_ReturnsFalse_IntVsString()
        {
            typeof(int).IsCompatibleTo(typeof(string)).Should().BeFalse();
        }

        [Test(Description = "Если в первом типе больше полей")]
        public void IsCompatible_ReturnsFalse_IfFirstClassHasMoreFields()
        {
            typeof(OneFieldClass).IsCompatibleTo(typeof(EmptyClass)).Should().BeFalse();
        }
        
        [Test(Description = "Если во втором типе больше полей")]
        public void IsCompatible_ReturnsFalse_IfSecondClassHasMoreFields()
        {
            typeof(EmptyClass).IsCompatibleTo(typeof(OneFieldClass)).Should().BeFalse();
        }
        
        [Test(Description = "Сравнение типов с одинаковым набором простых полей по имени и типу")]
        public void IsCompatible_ReturnsTrue_IfFieldsListsAreEqualByNameAndType()
        {
            typeof(OneFieldClass).IsCompatibleTo(typeof(OneFieldClassCompatible)).Should().BeTrue();
        }

        [Test(Description = "Сравнение типов с одинаковым набором простых полей по имени, но с разными типами")]
        public void IsCompatible_ReturnsFalse_IfFieldsListsAreEqualByNameButNotByType()
        {
            typeof(OneFieldClass).IsCompatibleTo(typeof(OneFieldClassNotCompatible)).Should().BeFalse();
        }

        [Test(Description = "Сравнение списков из совместимых типов")]
        public void IsCompatible_ReturnsTrue_ForTwoListsOfCompatibleTypes()
        {
            typeof(List<OneFieldClass>).IsCompatibleTo(typeof(List<OneFieldClassCompatible>)).Should().BeTrue();
        }

        [Test(Description = "Сравнение списков из несовместимых")]
        public void IsCompatible_ReturnsFalse_ForTwoListsOfNotCompatibleTypes()
        {
            typeof(List<OneFieldClass>).IsCompatibleTo(typeof(List<OneFieldClassNotCompatible>)).Should().BeFalse();
        }

        [Test(Description = "Сравнение сложных совместимых типов")]
        public void IsCompatible_ReturnsTrue_ComplexTest1()
        {
            typeof(ComplexClass).IsCompatibleTo(typeof(ComplexClassCompatible)).Should().BeTrue();
        }

        [Test(Description = "Сравнение сложных несовместимых типов")]
        public void IsCompatible_ReturnsFalse_ComplexTest1()
        {
            typeof(ComplexClass).IsCompatibleTo(typeof(ComplexClassNotCompatible)).Should().BeFalse();
        }
    }
}