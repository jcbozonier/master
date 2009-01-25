using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using SpecUnit;
using SqlFuck;
using SqlFuck.Tokens;

namespace SqlFuckTests
{
    [TestFixture]
    public class When_parsing_word_create : using_complex_create_statement
    {
        private VerbToken verbToken;
        protected override void Context()
        {
            Token = Scanner.GetNextToken();
            verbToken = (VerbToken)Token;
        }

        [Test]
        public void Should_be_parsed_as_a_verb()
        {
            Token.Is<VerbToken>();
        }

        [Test]
        public void Should_indicate_that_verb_is_create()
        {
            verbToken.Value.ShouldEqual("create");
        }
    }

    [TestFixture]
    public class When_parsing_word_table : using_complex_create_statement
    {
        private OptionToken optionToken;

        protected override void Context()
        {
            Token = Scanner.GetNextToken(2);
            optionToken = (OptionToken)Token;
        }

        [Test]
        public void Should_be_parsed_as_an_option()
        {
            Token.Is<OptionToken>();
        }

        [Test]
        public void Should_indicate_that_option_is_table()
        {
            optionToken.Value.ShouldEqual("table");
        }
    }

    [TestFixture]
    public class When_parsing_database_name : using_complex_create_statement
    {
        private ValueToken valueToken;

        protected override void Context()
        {
            Token = Scanner.GetNextToken(3);
            valueToken = (ValueToken)Token;
        }

        [Test]
        public void Should_be_parsed_as_a_value()
        {
            Token.Is<ValueToken>();
        }

        [Test]
        public void Should_indicate_that_value_is_database_name()
        {
            valueToken.Value.ShouldEqual("gameratings_db");
        }
    }
    
    [TestFixture]
    public class When_other : using_complex_create_statement
    {
        protected override void Context()
        {

        }

        [Test]
        public void Looking_for_child_accessor_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(4);

            Assert.IsInstanceOfType(typeof(ChildAccessorToken), token);
        }

        [Test]
        public void Looking_for_table_name_value_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(5);

            Assert.IsInstanceOfType(typeof(ValueToken), token);
            Assert.AreEqual(
                "testtable",
                ((ValueToken)token).Value,
                "The token's value should match the table name.");
        }

        [Test]
        public void Looking_for_tuple_start_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(6);

            Assert.IsInstanceOfType(typeof(ListStartToken), token);
        }

        [Test]
        public void Looking_for_column_name_value_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(7);

            Assert.IsInstanceOfType(typeof(ValueToken), token);
            Assert.AreEqual(
                "id",
                ((ValueToken)token).Value,
                "The token's value should match the table's column name.");
        }

        [Test]
        public void Looking_for_data_type_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(8);

            Assert.IsInstanceOfType(typeof(DataTypeToken), token);
            Assert.AreEqual(
                "bigint",
                ((DataTypeToken)token).Value,
                "The token's value should match the column's datatype name.");
        }

        [Test]
        public void Looking_for_data_type_length_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(10);

            Assert.IsInstanceOfType(typeof(ValueToken), token);
            Assert.AreEqual(
                "20",
                ((ValueToken)token).Value,
                "The token's value should match the column's datatype length value.");
        }

        [Test]
        public void Looking_for_list_end_token_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(11);

            Assert.IsInstanceOfType(typeof(ListEndToken), token);
        }

        [Test]
        public void Looking_for_default_option_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(12);

            Assert.IsInstanceOfType(typeof(OptionToken), token);
            Assert.AreEqual(
                "default",
                ((OptionToken)token).Value,
                "The token's value should match the default option's value.");
        }

        [Test]
        public void Looking_for_null_option_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(13);

            Assert.IsInstanceOfType(typeof(OptionToken), token);
            Assert.AreEqual(
                "null",
                ((OptionToken)token).Value,
                "The token's value should match the null option's value.");
        }

        [Test]
        public void Looking_for_list_item_separator_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(14);

            Assert.IsInstanceOfType(typeof(ListItemSeparatorToken), token);
        }

        [Test]
        public void Looking_for_varchar_data_type_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(16);

            Assert.IsInstanceOfType(typeof(OptionToken), token);
            Assert.AreEqual(
                "varchar",
                ((OptionToken)token).Value,
                "The token's value should match the default option's value.");
        }

        [Test]
        public void Looking_for_engine_option_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(23);

            Assert.IsInstanceOfType(typeof(OptionToken), token);
            Assert.AreEqual(
                "engine",
                ((OptionToken)token).Value,
                "The token's value should match the engine option's value.");
        }

        [Test]
        public void Looking_for_set_value_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(24);

            Assert.IsInstanceOfType(typeof(SetValueToken), token);
        }

        [Test]
        public void Looking_for_end_of_statement_token()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var scanner = new Scanner(sql);
            var token = scanner.GetNextToken(30);

            Assert.IsInstanceOfType(typeof(EndOfStatementToken), token);
        }

    }

    public abstract class using_complex_create_statement
    {
        protected Token Token { get; set; }
        protected Scanner Scanner { get; set; }

        [TestFixtureSetUp]
        public void Setup()
        {
            var sql = @"CREATE TABLE  'gameratings_db'.'testtable' (
                          'id' bigint(20) DEFAULT NULL,
                          'test' varchar(10) DEFAULT NULL
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            Scanner = new Scanner(sql);
            Context();
        }

        protected abstract void Context();
    }
}
