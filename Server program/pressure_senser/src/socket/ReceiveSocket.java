package socket;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;

import dataBase.DB_Connection;

public class ReceiveSocket extends Thread{
	Socket socket;
	int getCounter = 0;		//������յ������ݴ�С
	int bedNumber = 0; //��λ
	int state = 0 ; //״̬
	public ReceiveSocket(Socket s){
		this.socket = s;
	}
	
	public void run()
	{
		try {
			boolean connect = true;						//���ӳɹ��˵�
			byte getData[] = new byte[5];
			//��ͷ��2��+��λ��1��+״̬��1��+������1�� = 5���ֽ�
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
			  if(socket.isConnected()) //�ж��Ƿ���������
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
			  //�������ݿ�
			  
			  
			  //end
			  System.out.println(getCounter);	//��ӡ�����յ������ݸ���
			  if(getData[0] == -86 && getData[1] == -86) //0xaa����-86
			  {
				  bedNumber =getData[2];//�õ���λ��
				  state = getData[3];//�õ�״ֵ̬
				  System.out.println(bedNumber);
				  System.out.println(state);
				  DB_Connection a =new DB_Connection(bedNumber,state);
			  }
			  }
			  
				
			
		} catch (Exception e) {
			e.printStackTrace();//�������д�ӡ�쳣��Ϣ�ڳ����г����λ�ü�ԭ��
		}
	}
	
}
