using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

public class Connection
{
    private MySqlConnection _connect;

    private static Connection _instance;

    private StringSqlConnection _stringSqlConnection;

    private Connection()
    {
        _stringSqlConnection = new StringSqlConnection();
        _stringSqlConnection.Server = "188.120.236.212";
        _stringSqlConnection.Database = "Director_Himself";
        _stringSqlConnection.UserID = "Director_groot";
        _stringSqlConnection.Password = "1q2w3e4rQ";
        _stringSqlConnection.Pooling = true;

        Create();
    }

    public MySqlConnection Connect => _connect;

    public static Connection Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Connection();

            return _instance;
        }
    }

    private void Create()
    {
        _connect = new MySqlConnection();
        _connect.ConnectionString = 
            $"Server = {_stringSqlConnection.Server}; " +
            $"Database = {_stringSqlConnection.Database}; " +
            $"User ID = {_stringSqlConnection.UserID}; " +
            $"Password = {_stringSqlConnection.Password}; " +
            $"Pooling = {_stringSqlConnection.Pooling.ToString()}";
    }
}

class BaseSqlQuery
{
    private MySqlCommand _command;

    private DataTable _tableResult;

    public virtual void Execute()
    {
        var connect = Connection.Instance.Connect;
        CreateCommand();

        if (connect.State != ConnectionState.Open)
            connect.Open();

        CreateQuery();
    }

    protected virtual void CreateCommand()
    {
        _command = new MySqlCommand();
        _command.CommandType = CommandType.Text;
    }

    protected virtual void CreateQuery()
    {
        MySqlDataReader reader = _command.ExecuteReader();
        _tableResult = new DataTable();

        for (int i = 0; i < reader.FieldCount; i++)
            _tableResult.Columns.Add(reader.GetName(i));

        int countRows = 0;
        while (reader.Read())
        {
            object firstSubitem = new object();
            object[] subitems = new object[reader.FieldCount - 1];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (i == 0)
                    firstSubitem = reader.GetValue(i);
                else
                    subitems[i] = reader.GetValue(i);
            }

            _tableResult.Rows.Add(firstSubitem);
            _tableResult.Rows[countRows].ItemArray = subitems;

            ++countRows;
        }
    }
}

class SaveDataObjectCartoon : BaseSqlQuery
{
    
}
