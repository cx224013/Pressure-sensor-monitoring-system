package socket;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

import javax.swing.JOptionPane;


public class ServerListener  extends Thread{
	public void run() {
		try {
			ServerSocket serverSocket = new ServerSocket(9999); //�˿�
			while(true){
				//ÿ����һ���ͷ������Ӿ���һ��socket
				Socket socket =  serverSocket.accept();//����
				System.out.println("�пͷ�������-------");
				ReceiveSocket cs = new ReceiveSocket(socket);//���߳�
				cs.start();       	
			}
		} catch (IOException e) {
			e.printStackTrace();
		} 
	}
}
