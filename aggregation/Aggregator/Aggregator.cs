using Aggregator.Connection;
using Vertica.Data.VerticaClient;

namespace Aggregator.Aggregation
{
    public class DbAggregator : IAggregator
    {
        private VerticaCommand              query;
        private List<string>?               queries = new List<string> {
            "CREATE TABLE TRANS_MW_ERC_PM_JOIN AS \r\n        SELECT \r\n                T1.NETWORK_SID,\r\n                T1.DATETIME_KEY,\r\n                T1.NEID,\r\n                T1.\"TIME\" AS DATETIME_VALUE,\r\n                T1.NEALIAS,\r\n                T1.NETYPE,\r\n                T1.MAXRXLEVEL,\r\n                T2.RFINPUTPOWER,\r\n                T1.LINK,\r\n                T1.TID,\r\n                T1.FARENDTID,\r\n                T2.SLOT,\r\n                T2.PORT\r\n        FROM TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER T1\r\n        JOIN TRANS_MW_ERC_PM_WAN_RFINPUTPOWER T2\r\n        ON T1.NETWORK_SID = T2.NETWORK_SID\r\n;\r\n",
            "INSERT INTO TRANS_MW_AGG_SLOT_HOURLY\r\n        SELECT\r\n                DATETIME_KEY,\r\n                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),\r\n                NETWORK_SID,\r\n                NEALIAS,\r\n                NETYPE,\r\n                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,\r\n                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,\r\n                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)\r\n        FROM    \r\n                TRANS_MW_ERC_PM_JOIN\r\n        GROUP BY\r\n                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),\r\n                NETWORK_SID,\r\n                DATETIME_KEY,\r\n                NEALIAS,\r\n                NETYPE\r\n        ORDER BY \r\n                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),\r\n                NETWORK_SID,\r\n                DATETIME_KEY,\r\n                NEALIAS,\r\n                NETYPE\r\n;",
            "INSERT INTO TRANS_MW_AGG_SLOT_DAILY\r\n        SELECT\r\n                DATETIME_KEY,\r\n                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),\r\n                NETWORK_SID,\r\n                NEALIAS,\r\n                NETYPE,\r\n                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,\r\n                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,\r\n                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)\r\n        FROM    \r\n                TRANS_MW_ERC_PM_JOIN\r\n        GROUP BY\r\n                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),\r\n                NETWORK_SID,\r\n                DATETIME_KEY,\r\n                NEALIAS,\r\n                NETYPE\r\n        ORDER BY \r\n                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),\r\n                NETWORK_SID,\r\n                DATETIME_KEY,\r\n                NEALIAS,\r\n                NETYPE\r\n;",
            "INSERT INTO TRANS_MW_AGG_SLOT_ALL_TIME\r\n        SELECT\r\n                DATETIME_KEY,\r\n                NETWORK_SID,\r\n                NEALIAS,\r\n                NETYPE,\r\n                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,\r\n                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,\r\n                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)\r\n        FROM    \r\n                TRANS_MW_ERC_PM_JOIN\r\n        GROUP BY\r\n                NETWORK_SID,\r\n                DATETIME_KEY,\r\n                NEALIAS,\r\n                NETYPE\r\n        ORDER BY\r\n                NETWORK_SID,\r\n                DATETIME_KEY,\r\n                NEALIAS,\r\n                NETYPE\r\n;",
            "INSERT INTO TRANS_MW_AGG_SLOT_HOURLY_NETYPE\r\n        SELECT\r\n                DATETIME_KEY,\r\n                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),\r\n                NETYPE,\r\n                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,\r\n                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,\r\n                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)\r\n        FROM    \r\n                TRANS_MW_ERC_PM_JOIN\r\n        GROUP BY\r\n                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),\r\n                DATETIME_KEY,\r\n                NETYPE\r\n        ORDER BY \r\n                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),\r\n                DATETIME_KEY,\r\n                NETYPE\r\n;",
            "INSERT INTO TRANS_MW_AGG_SLOT_HOURLY_NEALIAS\r\n        SELECT\r\n                DATETIME_KEY,\r\n                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),\r\n                NEALIAS,\r\n                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,\r\n                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,\r\n                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)\r\n        FROM    \r\n                TRANS_MW_ERC_PM_JOIN\r\n        GROUP BY\r\n                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),\r\n                DATETIME_KEY,\r\n                NEALIAS\r\n        ORDER BY \r\n                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),\r\n                DATETIME_KEY,\r\n                NEALIAS\r\n;",
            "INSERT INTO TRANS_MW_AGG_SLOT_DAILY_NETYPE\r\n        SELECT\r\n                DATETIME_KEY,\r\n                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),\r\n                NETYPE,\r\n                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,\r\n                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,\r\n                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)\r\n        FROM    \r\n                TRANS_MW_ERC_PM_JOIN\r\n        GROUP BY\r\n                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),\r\n                DATETIME_KEY,\r\n                NETYPE\r\n        ORDER BY \r\n                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),\r\n                DATETIME_KEY,\r\n                NETYPE\r\n;",
            "INSERT INTO TRANS_MW_AGG_SLOT_DAILY_NEALIAS\r\n        SELECT\r\n                DATETIME_KEY,\r\n                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),\r\n                NEALIAS,\r\n                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,\r\n                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,\r\n                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)\r\n        FROM    \r\n                TRANS_MW_ERC_PM_JOIN\r\n        GROUP BY\r\n                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),\r\n                DATETIME_KEY,\r\n                NEALIAS\r\n        ORDER BY \r\n                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),\r\n                DATETIME_KEY,\r\n                NEALIAS\r\n;",
            "INSERT INTO TRANS_MW_AGG_SLOT_NETYPE\r\n        SELECT\r\n                DATETIME_KEY,\r\n                NETYPE,\r\n                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,\r\n                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,\r\n                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)\r\n        FROM    \r\n                TRANS_MW_ERC_PM_JOIN\r\n        GROUP BY\r\n                DATETIME_KEY,\r\n                NETYPE\r\n        ORDER BY \r\n                DATETIME_KEY,\r\n                NETYPE\r\n;",
            "INSERT INTO TRANS_MW_AGG_SLOT_NEALIAS\r\n        SELECT\r\n                DATETIME_KEY,\r\n                NEALIAS,\r\n                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,\r\n                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,\r\n                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)\r\n        FROM    \r\n                TRANS_MW_ERC_PM_JOIN\r\n        GROUP BY\r\n                DATETIME_KEY,\r\n                NEALIAS\r\n        ORDER BY\r\n                DATETIME_KEY,\r\n                NEALIAS\r\n;",
            "DROP TABLE IF EXISTS TRANS_MW_ERC_PM_JOIN;",
            "DROP TABLE IF EXISTS TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER;",
            "DROP TABLE IF EXISTS TRANS_MW_ERC_PM_WAN_RFINPUTPOWER;",
            "CREATE TABLE TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER (\r\n        NETWORK_SID INTEGER,\r\n        DATETIME_KEY INTEGER,\r\n        NEID FLOAT,\r\n        \"OBJECT\" VARCHAR,\r\n        \"TIME\" DATETIME,\r\n        \"INTERVAL\" INTEGER,\r\n        DIRECTION VARCHAR,\r\n        NEALIAS VARCHAR,\r\n        NETYPE VARCHAR,\r\n        RXLEVELBELOWTS1 VARCHAR,\r\n        RXLEVELBELOWTS2 VARCHAR,\r\n        MINRXLEVEL FLOAT,\r\n        MAXRXLEVEL FLOAT,\r\n        TXLEVELABOVETS1 VARCHAR,\r\n        MINTXLEVEL FLOAT,\r\n        MAXTXLEVEL FLOAT,\r\n        FAILUREDESCRIPTION VARCHAR,\r\n        LINK VARCHAR,\r\n        TID VARCHAR,\r\n        FARENDTID VARCHAR,\r\n        SLOT VARCHAR,\r\n        PORT VARCHAR\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID, DATETIME_KEY) ALL NODES KSAFE 1\r\n        PARTITION BY time_slice(TIME, 1, 'HOUR', 'END')\r\n        GROUP BY CASE\r\n                WHEN DATEDIFF(HOUR, time_slice(TIME, 1, 'HOUR', 'END'), NOW()) >=24\r\n                THEN DATE_TRUNC('DAY', time_slice(TIME, 1, 'HOUR', 'END'))\r\n                ELSE time_slice(TIME, 1, 'HOUR', 'END')\r\n                END;",
            "CREATE TABLE TRANS_MW_ERC_PM_WAN_RFINPUTPOWER (\r\n        NETWORK_SID INTEGER,\r\n        DATETIME_KEY INTEGER,\r\n        NODENAME VARCHAR,\r\n        NEID FLOAT,\r\n        \"OBJECT\" VARCHAR,\r\n        \"TIME\" DATETIME,\r\n        \"INTERVAL\" INTEGER,\r\n        DIRECTION VARCHAR,\r\n        NEALIAS VARCHAR,\r\n        NETYPE VARCHAR,\r\n        RFINPUTPOWER FLOAT,\r\n        TID VARCHAR,\r\n        FARENDTID VARCHAR,\r\n        SLOT VARCHAR,\r\n        PORT VARCHAR\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID, DATETIME_KEY) ALL NODES KSAFE 1\r\n        PARTITION BY time_slice(TIME, 24, 'HOUR', 'END')\r\n        GROUP BY CASE\r\n                WHEN DATEDIFF(DAY, time_slice(TIME, 24, 'HOUR', 'END'), NOW()) >=7\r\n                THEN DATE_TRUNC('WEEK', time_slice(TIME, 24, 'HOUR', 'END'))\r\n                ELSE time_slice(TIME, 24, 'HOUR', 'END')\r\n                END;"
        };
        private readonly IDbConnection      _connection;

        public DbAggregator(IDbConnection connection)
        {
            _connection = connection;

            query = _connection.QueryCommand();

            ProcessQueries();

            Console.WriteLine("Aggregation Started!");
        }

        public void ProcessQueries()
        {
            for (int i = 0; i < queries!.Count; i++)
            {
                try
                {
                    query.CommandText = queries[i] + ';';

                    query.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
