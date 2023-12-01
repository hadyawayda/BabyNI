using Loader.Connection;
using Vertica.Data.VerticaClient;

namespace Loader.Services
{
    public class DbInitializer : IDbInitalizer
    {
        private VerticaCommand query;
        private List<string>? queries = new List<string> { "DROP TABLE IF EXISTS TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER;",
                "DROP TABLE IF EXISTS TRANS_MW_ERC_PM_WAN_RFINPUTPOWER;",
                "DROP TABLE IF EXISTS TRANS_MW_ERC_PM_JOIN;",
                "DROP TABLE IF EXISTS TRANS_MW_AGG_SLOT_HOURLY;",
                "DROP TABLE IF EXISTS TRANS_MW_AGG_SLOT_DAILY;",
                "DROP TABLE IF EXISTS TRANS_MW_AGG_SLOT_ALL_TIME;",
                "DROP TABLE IF EXISTS TRANS_MW_AGG_SLOT_HOURLY_NETYPE;",
                "DROP TABLE IF EXISTS TRANS_MW_AGG_SLOT_HOURLY_NEALIAS;",
                "DROP TABLE IF EXISTS TRANS_MW_AGG_SLOT_DAILY_NETYPE;",
                "DROP TABLE IF EXISTS TRANS_MW_AGG_SLOT_DAILY_NEALIAS;",
                "DROP TABLE IF EXISTS TRANS_MW_AGG_SLOT_NETYPE;",
                "DROP TABLE IF EXISTS TRANS_MW_AGG_SLOT_NEALIAS;",
                "CREATE TABLE TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER (\r\n        NETWORK_SID INTEGER,\r\n        DATETIME_KEY INTEGER,\r\n        NEID FLOAT,\r\n        \"OBJECT\" VARCHAR,\r\n        \"TIME\" DATETIME,\r\n        \"INTERVAL\" INTEGER,\r\n        DIRECTION VARCHAR,\r\n        NEALIAS VARCHAR,\r\n        NETYPE VARCHAR,\r\n        RXLEVELBELOWTS1 FLOAT,\r\n        RXLEVELBELOWTS2 FLOAT,\r\n        MINRXLEVEL FLOAT,\r\n        MAXRXLEVEL FLOAT,\r\n        TXLEVELABOVETS1 FLOAT,\r\n        MINTXLEVEL FLOAT,\r\n        MAXTXLEVEL FLOAT,\r\n        FAILUREDESCRIPTION VARCHAR,\r\n        LINK VARCHAR,\r\n        TID VARCHAR,\r\n        FARENDTID VARCHAR,\r\n        SLOT VARCHAR,\r\n        PORT VARCHAR\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID, DATETIME_KEY) ALL NODES KSAFE 1\r\n        PARTITION BY time_slice(TIME, 1, 'HOUR', 'END')\r\n        GROUP BY CASE\r\n                WHEN DATEDIFF(HOUR, time_slice(TIME, 1, 'HOUR', 'END'), NOW()) >=24\r\n                THEN DATE_TRUNC('DAY', time_slice(TIME, 1, 'HOUR', 'END'))\r\n                ELSE time_slice(TIME, 1, 'HOUR', 'END')\r\n                END;",
                "CREATE TABLE TRANS_MW_ERC_PM_WAN_RFINPUTPOWER (\r\n        NETWORK_SID INTEGER,\r\n        DATETIME_KEY INTEGER,\r\n        NODENAME VARCHAR,\r\n        NEID FLOAT,\r\n        \"OBJECT\" VARCHAR,\r\n        \"TIME\" DATETIME,\r\n        \"INTERVAL\" INTEGER,\r\n        DIRECTION VARCHAR,\r\n        NEALIAS VARCHAR,\r\n        NETYPE VARCHAR,\r\n        RFINPUTPOWER FLOAT,\r\n        TID VARCHAR,\r\n        FARENDTID VARCHAR,\r\n        SLOT VARCHAR,\r\n        PORT VARCHAR\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID, DATETIME_KEY) ALL NODES KSAFE 1\r\n        PARTITION BY time_slice(TIME, 24, 'HOUR', 'END')\r\n        GROUP BY CASE\r\n                WHEN DATEDIFF(DAY, time_slice(TIME, 24, 'HOUR', 'END'), NOW()) >=7\r\n                THEN DATE_TRUNC('WEEK', time_slice(TIME, 24, 'HOUR', 'END'))\r\n                ELSE time_slice(TIME, 24, 'HOUR', 'END')\r\n                END;",
                "CREATE TABLE TRANS_MW_AGG_SLOT_HOURLY \r\n        (\r\n                DATETIME_KEY INTEGER,\r\n                \"TIME\" DATETIME,\r\n                NETWORK_SID INTEGER,\r\n                NEALIAS VARCHAR,\r\n                NETYPE VARCHAR,\r\n                RSL_INPUT_POWER FLOAT,\r\n                MAX_RX_LEVEL FLOAT,\r\n                RSL_DEVIATION FLOAT\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID, \"TIME\") ALL NODES KSAFE 1;",
                "CREATE TABLE TRANS_MW_AGG_SLOT_DAILY \r\n        (\r\n                DATETIME_KEY INTEGER,\r\n                \"TIME\" DATETIME,\r\n                NETWORK_SID INTEGER,\r\n                NEALIAS VARCHAR,\r\n                NETYPE VARCHAR,\r\n                RSL_INPUT_POWER FLOAT,\r\n                MAX_RX_LEVEL FLOAT,\r\n                RSL_DEVIATION FLOAT\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID, \"TIME\") ALL NODES KSAFE 1;",
                "CREATE TABLE TRANS_MW_AGG_SLOT_HOURLY_NETYPE \r\n        (\r\n                DATETIME_KEY INTEGER,\r\n                \"TIME\" DATETIME,\r\n                NETYPE VARCHAR,\r\n                RSL_INPUT_POWER FLOAT,\r\n                MAX_RX_LEVEL FLOAT,\r\n                RSL_DEVIATION FLOAT\r\n        )\r\n        SEGMENTED BY HASH(NETYPE, \"TIME\") ALL NODES KSAFE 1;",
                "CREATE TABLE TRANS_MW_AGG_SLOT_HOURLY_NEALIAS \r\n        (\r\n                DATETIME_KEY INTEGER,\r\n                \"TIME\" DATETIME,\r\n                NEALIAS VARCHAR,\r\n                RSL_INPUT_POWER FLOAT,\r\n                MAX_RX_LEVEL FLOAT,\r\n                RSL_DEVIATION FLOAT\r\n        )\r\n        SEGMENTED BY HASH(NEALIAS, \"TIME\") ALL NODES KSAFE 1;",
                "CREATE TABLE TRANS_MW_AGG_SLOT_DAILY_NETYPE \r\n        (\r\n                DATETIME_KEY INTEGER,\r\n                \"TIME\" DATETIME,\r\n                NETYPE VARCHAR,\r\n                RSL_INPUT_POWER FLOAT,\r\n                MAX_RX_LEVEL FLOAT,\r\n                RSL_DEVIATION FLOAT\r\n        )\r\n        SEGMENTED BY HASH(NETYPE, \"TIME\") ALL NODES KSAFE 1;",
                "CREATE TABLE TRANS_MW_AGG_SLOT_DAILY_NEALIAS\r\n        (\r\n                DATETIME_KEY INTEGER,\r\n                \"TIME\" DATETIME,\r\n                NEALIAS VARCHAR,\r\n                RSL_INPUT_POWER FLOAT,\r\n                MAX_RX_LEVEL FLOAT,\r\n                RSL_DEVIATION FLOAT\r\n        )\r\n        SEGMENTED BY HASH(NEALIAS, \"TIME\") ALL NODES KSAFE 1;",
                "CREATE TABLE TRANS_MW_AGG_SLOT_ALL_TIME\r\n        (\r\n                DATETIME_KEY INTEGER,\r\n                NETWORK_SID INTEGER,\r\n                NEALIAS VARCHAR,\r\n                NETYPE VARCHAR,\r\n                RSL_INPUT_POWER FLOAT,\r\n                MAX_RX_LEVEL FLOAT,\r\n                RSL_DEVIATION FLOAT\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID) ALL NODES KSAFE 1;",
                "CREATE TABLE TRANS_MW_AGG_SLOT_NETYPE\r\n        (\r\n                DATETIME_KEY INTEGER,\r\n                NETYPE VARCHAR,\r\n                RSL_INPUT_POWER FLOAT,\r\n                MAX_RX_LEVEL FLOAT,\r\n                RSL_DEVIATION FLOAT\r\n        )\r\n        SEGMENTED BY HASH(NETYPE) ALL NODES KSAFE 1;",
                "CREATE TABLE TRANS_MW_AGG_SLOT_NEALIAS\r\n        (\r\n                DATETIME_KEY INTEGER,\r\n                NEALIAS VARCHAR,\r\n                RSL_INPUT_POWER FLOAT,\r\n                MAX_RX_LEVEL FLOAT,\r\n                RSL_DEVIATION FLOAT\r\n        )\r\n        SEGMENTED BY HASH(NEALIAS) ALL NODES KSAFE 1;"
            };
        private readonly IDbConnection _connection;

        public DbInitializer(IDbConnection connection)
        {
            _connection = connection;

            query = _connection.QueryCommand();

            ProcessQueries();
        }

        public void ProcessQueries()
        {
            for (int i = 0; i < queries!.Count; i++)
            {
                try
                {
                    query.CommandText = queries[i];

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
