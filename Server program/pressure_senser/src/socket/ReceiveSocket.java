package socket;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;

import javax.swing.DefaultDesktopManager;

import dataBase.DB_Connection;

public class ReceiveSocket extends Thread{
	Socket socket;
	int getCounter = 0;		//定义接收到的数据大小
	int bedNumber = 0; //床位
	int state = 0 ; //状态
	
	public ReceiveSocket(Socket s){
		this.socket = s;
	}
	
	public void run()
	{
		try {
			boolean connect = true;						//连接成功了的
			byte getData[] = new byte[5];
			//包头（2）+床位（1）+状态（1）+保留（1） = 5个字节
			InputStream isTemplet = null;
			OutputStream osTemplet = null;
			
			while(true)
			{				
				try {
					osTemplet = socket.getOutputStream();
					isTemplet = socket.getInputStream();
					
				} catch (Exception e) {
					connect = false ;
					System.out.println("Broken A");
					break;
				}
			  if(socket.isConnected()) //判断是否在连接中
			  {
					try
					{
						getCounter=isTemplet.read(getData, 0, 5);						
					}
					catch(IOException e)
					{
						e.printStackTrace();
						System.out.println("Broken B");
						connect = false;
						break; 
					}
			  }
			  
			  //如果接受到的是床位状态信息			  			  
			  if(getData[0] == -86 && getData[1] == -86) //0xaa就是-86
			  {
				  bedNumber =getData[2];//得到床位数
				  state = getData[3];//得到状态值
				  System.out.println(bedNumber);
				  System.out.println(state);
				  DB_Connection DB =new DB_Connection();
				  DB.insert_bed_state(bedNumber, state);
				  DB.close();
				  getData[0] = 0;  //防止再次进入
			  }
			  
			  //如果接收到的是报警信息
			  if(getData[0] == -86 && getData[1] == 17) //0xaa就是-86
			  {
				  //检查是否允许报警
				  //设置该床位的报警状态
				  //告诉下位机是否能给预警模块发送报警信息
			  }
			  
			  }
			  
				
			
		} catch (Exception e) {
			e.printStackTrace();//在命令行打印异常信息在程序中出错的位置及原因
		}
	}
	
}
