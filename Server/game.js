const _ = require('lodash')
const placeholders = require('./placeholders')

class Game {
  constructor () {
    this.connections = []
  }

  send (type, body) {
    _.forEach(this.connections, conn => conn.send(type, body))
  }

  addConnection (conn) {
    this.connections.push(conn)
    conn.game = this
  }

  start () {
    var map = Math.random() >= 0.5 ? placeholders.mapOne : placeholders.mapTwo
    this.send('MAPP', JSON.stringify(map))
    this.send('ARMY', `0|${JSON.stringify(placeholders.armyOne)}`, 1500)
    this.send('ARMY', `1|${JSON.stringify(placeholders.armyTwo)}`, 1500)
    this.send('TURN', '88', 1000)
  }

  end () {
    _.forEach(this.connections, conn => delete conn.game)
    this.send('DISC')
  }
}

module.exports = Game
