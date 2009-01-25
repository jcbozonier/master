using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SqlFuck;
using NUnit.Framework;

namespace SqlFuckTests
{
    [TestFixture]
    public class PublicApiTests
    {

        [Test]
        public void Ideal_Simple_Usage()
        {
            var originalSql = @"CREATE TABLE  `gameratings_db`.`testtable` (
  `id` bigint(20) DEFAULT NULL,
  `test` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            var newSql = @"CREATE TABLE  `gameratings_db`.`testtable` (
  `id` bigint(30) DEFAULT NULL,
  `test` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
            
            var upgradePath = new SqlPatch().From(originalSql).To(newSql);

            var correctPath = @"ALTER TABLE 'gameratings_db'.'testtable'
 MODIFY id   bigint(30) DEFAULT NULL";

            Assert.AreEqual(correctPath, upgradePath, "The paths differ. Almost there though!");
        }
    }
}
