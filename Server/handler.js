const matchmaking = require('./matchmaking')

const message = (conn, message) => {
  var split = message.split('|')
  var type = split.shift()
  var body = split.join('|')
  console.log(message)
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
      conn.game ? conn.game.send(type, body) : conn.send(type, body)
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
