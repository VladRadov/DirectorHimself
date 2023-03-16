using System;
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
    protected MySqlCommand _command;

    protected DataTable _tableResult;

    public virtual void Execute()
    {
        var connect = Connection.Instance.Connect;
        _command = Connection.Instance.Connect.CreateCommand();

        if (connect.State != ConnectionState.Open)
            connect.Open();

        CreateCommand();
        CreateQuery();
    }

    protected virtual void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
    }

    protected virtual void CreateQuery()
    {
        using (MySqlDataReader reader = _command.ExecuteReader())
        {
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

    public int CounItems() => _tableResult.Rows.Count;

    public int CounColumns() => _tableResult.Columns.Count;

    public object ParsingTableResult(int indexRow, int indexColumn) => _tableResult.Rows[indexRow].ItemArray[indexColumn];
}

class AddPlayerLogin : BaseSqlQuery
{
    private string _nickname;

    private string _email;

    private int _idPlayer;

    public AddPlayerLogin(string nickname, string email)
    {
        _nickname = nickname;
        _email = email;
    }

    public int IdPLayer => _idPlayer;

    public override void Execute() => base.Execute();

    protected override void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
        _command.CommandText = "AddPlayer";
        _command.Parameters.AddWithValue("InNickname", _nickname);
        _command.Parameters.AddWithValue("InEmail", _email);
        _command.Parameters.AddWithValue("IdPlayer", _idPlayer).Direction = ParameterDirection.Output;
    }

    protected override void CreateQuery() => base.CreateQuery();
}

class GetCartoons : BaseSqlQuery
{
    private string _nickname;

    private string _email;

    public GetCartoons(string nickname, string email)
    {
        _nickname = nickname;
        _email = email;
    }

    public override void Execute() => base.Execute();

    protected override void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
        _command.CommandText = "GetCartoons";
        _command.Parameters.AddWithValue("NickPlayer", _nickname);
        _command.Parameters.AddWithValue("EmailPlayer", _email);
    }

    protected override void CreateQuery() => base.CreateQuery();
}
