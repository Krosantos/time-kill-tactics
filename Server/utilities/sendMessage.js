const _ = require('lodash')
const crypto = require('crypto')

// The client accepts packets in chunks of 1028 bytes. In order to send large messages, we need to break down
// their bodies into annotated snippets, with an order, id, type, and "isFinal" flag.

// We reserve 32 bytes for header data. The remaining 996 bytes are for the body.
module.exports = (type, body, connection) => {
  const chunked = _.chunk(body, 996)
  const id = crypto.randomBytes(Math.ceil(9)).toString('hex').slice(0, 18)

  _.forEach(chunked, (snippet, index) => {
    var packet = _formatPacket(type, id, index, index + 1 === chunked.length, snippet)
    connection.write(packet)
  })
}

const _formatPacket = (type, id, order, isFinal, snippet) => `${type}|${id}|${_.padStart(order.toString(), 5, '0')}|${isFinal ? '1' : '0'}|${snippet.join('')}`
