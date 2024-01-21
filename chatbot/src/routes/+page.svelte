<script>
	import { onMount } from 'svelte';
	import { startConnection, sendMessage, onMessageReceived } from '$lib/signalr';

	let messages = [];
	let newMessage = '';

	const sendMessageHandler = () => {
		messages = [
			...messages,
			{
				sender: 'me',
				text: newMessage
			}
		];
		sendMessage(newMessage);
		newMessage = '';
	};

	onMount(async () => {
		await startConnection();

		onMessageReceived((message) => {
			let responseText = 'Uh-oh, not sure I got that.';
			let messageJsonString = JSON.parse(message)?.choices[0].message.content;

			try {
				responseText = JSON.parse(messageJsonString).content;
			} catch (error) {
				responseText = messageJsonString === '' ? responseText : messageJsonString;
			}

			let messageDto = {
				sender: 'bot',
				text: responseText
			};

			messages = [...messages, messageDto];
		});
	});
</script>

<main>
	{#each messages as message}
		<div class={`message-${message.sender}`}>{message.text}</div>
	{/each}

	<div class="input-container">
		<input bind:value={newMessage} placeholder="Type your message..." />
		<button on:click={sendMessageHandler}>Send</button>
	</div>
</main>

<style>
	* {
		font-family:
			system-ui,
			-apple-system,
			BlinkMacSystemFont,
			'Segoe UI',
			Roboto,
			Oxygen,
			Ubuntu,
			Cantarell,
			'Open Sans',
			'Helvetica Neue',
			sans-serif;
	}
	:global(body) {
		margin: 0;
		background-color: #111;
	}
	main {
		height: calc(100vh - 1rem);
		padding: .5rem;
		display: flex;
		flex-direction: column;
		justify-content: flex-end;
	}

	.message-me {
		align-self: flex-end;
		background-color: #297afe;
	}

	.message-bot {
		background-color: #81b1ff;
		align-self: flex-start;
	}

	.message-me,
	.message-bot {
		padding: 1rem;
		margin-bottom: .5rem;
		color: white;
		border-radius: .25rem;
		max-width: 75%;
	}

	.input-center {
		display: flex;
		align-self: end;
		width: 100%;
		padding: 1rem;
	}

	.input-container {
		margin-top: 1rem;
		width: 100%;
		justify-self: flex-end;
		display: flex;
		align-self: flex-end;
	}

	input {
		flex-grow: 1;
		padding: 1rem;
		background-color: #222;
		color: white;
		border: none;
		border-radius: .25rem;
	}

	button {
		background-color: #297afe;
		border: none;
		border-radius: .25rem;
		color: white;
		padding: 0 1rem;
		margin-left: .5rem;
	}
</style>
