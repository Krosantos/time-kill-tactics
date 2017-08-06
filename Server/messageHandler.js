const matchmaking = require('./matchmaking')

module.exports = (conn, message) => {
  console.log(message)
  var type = message.split('|')[0]
  switch (type) {
    case 'FIND':
      // Add to matchmaking queue
      matchmaking.enqueue(conn)
      break
    default :
      // The default behaviour (for now) is to echo to the entire group/game.
      conn.game ? conn.game.send(message) : conn.send(message)
      break
  }
}
