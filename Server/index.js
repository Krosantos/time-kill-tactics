const net = require('net')
const Promise = require('bluebird')

var index = 0

const server = net.createServer(conn => {
  conn.setEncoding('utf8')
  console.log('client connected')
  conn.on('end', () => console.log('client disconnected'))
  conn.on('data', data => {
    console.log(data)
    repeater(conn)
  })
  conn.on('error', err => console.log(err))
})

server.on('error', (err) => {
  console.log(err)
  throw err
})
server.listen(3000, () => {
  console.log('Server listening!')
})

function repeater (conn) {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      conn.write(`Boom! ${index}`)
      console.log(`Boom! ${index}`)
      index++
      resolve()
    }, 5000)
  })
    .then(() => repeater(conn))
}
