<script>
  import { onMount } from 'svelte';
  import { startConnection, sendMessage, onMessageReceived } from '$lib/signalr';

  let messages = [];
  let newMessage = '';

  const sendMessageHandler = () => {
    messages = [...messages, {
    sender: 'me',
    text: newMessage
  }];
    sendMessage(newMessage);
    newMessage = '';
  };

  onMount(async () => {
    await startConnection();



    onMessageReceived((message) => {

        let responseText = 'Uh-oh, not sure I got that.';
        let messageJsonString = JSON.parse(message)?.choices[0].message.content

        try {
            responseText = JSON.parse(messageJsonString).content
        }
        catch(error) {
            responseText = messageJsonString === '' ? responseText : messageJsonString
        }

        let messageDto = {
          sender: 'bot',
          text: responseText
        }

      messages = [...messages, messageDto];
    });
  });
</script>

<style>
  /* Add your dark mode styles here */
</style>

<main>
  {#each messages as message}
    <div class={`message-${message.sender}`}>{message.text}</div>
  {/each}

  <div class="input-container">
    <input bind:value={newMessage} placeholder="Type your message..." />
    <button on:click={sendMessageHandler}>Send</button>
  </div>
</main>
