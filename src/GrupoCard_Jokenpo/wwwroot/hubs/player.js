const con = new signalR.HubConnectionBuilder()
    .withUrl('hubs/player')
    .build();

con.start().then(() => {
    console.log('connection stable');
});

con.on('PlayerConnected', (user) => {
    document.querySelector('.index_players').innerHTML += '<p name="' + user + '">' + user + '</p>';
    document.querySelector('.index_board_text').innerHTML += '<p class="message green_message">Jogador: ' + user + ', conectado</p>';
    scroll_message();
});

con.on('PlayerDisconnected', (user) => {
    var players = document.querySelector('.index_players');
    document.querySelector('.index_board_text').innerHTML += '<p class="message red_message">Jogador: ' + user + ', desconectado</p>';

    var childs = document.getElementsByName(user);

    childs.forEach(child => {
        players.removeChild(child);
    });
    scroll_message();
});

con.on('Result', (winners) => {
    document.querySelector('.index_board_text').innerHTML += '<p class="message winner_message">Vencedores: ' + winners + '</p>';
    document.querySelector('.counter').value = 30;
    scroll_message();
});


const scroll_message = () => {
    $('.index_board_text').animate({
        scrollTop: $('.index_board_text')[0].scrollHeight
    }, 'slow');
}