import { HubConnectionBuilder } from '@microsoft/signalr';

const connection = new HubConnectionBuilder()
  .withUrl('http://localhost:5173/chathub')
  .build();

export const startConnection = async () => {
  try {
    await connection.start();
    console.log('SignalR Connected!');
  } catch (err) {
    console.error('Error connecting to SignalR:', err);
  }
};

export const sendMessage = (message) => {
  connection.invoke('SendMessage', message);
};

export const onMessageReceived = (callback) => {
  connection.on('ReceiveMessage', (message) => {
    console.log(message);
    callback(message);
  });
};
