package dataBase;

import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.ResultSet;
import java.sql.DriverManager;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.*;

public class DB_Connection {
	String Connection="jdbc:mysql://localhost:3306/RY?"+
			"user=root&password=123456&characterEncoding=UTF8";
	String uri = "jdbc:mysql://localhost:3306/RY?";
	String user = "user=root&password=123456&characterEncoding=UTF8";
	
	 String connet = "jdbc:mysql://120.27.119.115:3306/RY?"
             + "user=root&password=123456&useUnicode=true&characterEncoding=UTF8";
	String inSql = null;
	public DB_Connection(int bedNumber,int state)
	{
		java.sql.Connection conn = null;
		java.sql.Statement stmt =null;
		//ע������
		try {  
            Class.forName("com.mysql.jdbc.Driver");  
        } catch (ClassNotFoundException e) {  
        	System.out.println("Broken driver");
            e.printStackTrace();  
        }  
		//��������
		 try {
			 conn = DriverManager.getConnection(connet);
			 //��ȡ���ʽ
			 stmt= conn.createStatement();
			 
		} catch (SQLException e) {
			System.out.println("Broken conn");
			e.printStackTrace();
		}
		 
		 Date date = new Date();//���ϵͳʱ��.
         String nowTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(date);//��ʱ���ʽת���ɷ���TimestampҪ��ĸ�ʽ.
         Timestamp goodsC_date = Timestamp.valueOf(nowTime);//��ʱ��ת��
         
		 if(state ==1)
			 inSql = "insert into message(bed_Number,state,Dtime) values('" + bedNumber + "','back','" + goodsC_date + "')";
		 else {
			 inSql = "insert into message(bed_Number,state,Dtime) values('" + bedNumber + "','leave','" + goodsC_date + "')";
		}
		 try {
			stmt.executeUpdate(inSql);
		} catch (SQLException e) {
			System.out.println("Broken insert");
			e.printStackTrace();
		} 
		 
	    try {
			stmt.close();
			 conn.close(); 
		} catch (SQLException e) {
			System.out.println("Broken close");
			e.printStackTrace();
		}  
	  
		
	}
	
	
	
	
	
}
