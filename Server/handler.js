const matchmaking = require('./matchmaking')

const message = (conn, message) => {
  console.log(message)
  var type = message.split('|')[0]
  switch (type) {
    case 'FIND':
      // Add to matchmaking queue
      matchmaking.enqueue(conn)
      break
    case 'ARMY':
      // Add the enclosed army to the player?
      break
    default :
      // The default behaviour (for now) is to echo to the entire group/game.
      conn.game ? conn.game.send(message) : conn.send(message)
      break
  }
}

const disconnect = (conn) => {

}

const error = (conn, error) => {

}

module.exports = {
  message: message,
  disconnect: disconnect,
  error: error
}
