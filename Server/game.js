const _ = require('lodash')

class Game {
  constructor () {
    this.connections = []
  }

  send (msg) {
    _.forEach(this.connections, conn => conn.send(msg))
  }

  addConnection (conn) {
    this.connections.push(conn)
    conn.game = this
  }

  end () {
    _.forEach(this.connections, conn => delete conn.game)
    this.send('DISC')
  }
}

module.exports = Game
