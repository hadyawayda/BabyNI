interface Data {
   datetimE_KEY: number
   time: Date
   networK_SID: number
   nealias: string
   netype: string
   rsL_INPUT_POWER: number
   maX_RX_LEVEL: number
   rsL_DEVIATION: number
}

const Chart = ({ props }: { props: Data[] }) => {
   return (
      <>
         <div className="grid grid-cols-4 gap-8">
            {props.map((prop: Data) => (
               <ul className="grid">
                  <li key={prop.datetimE_KEY}>
                     Datetime Key: {prop.datetimE_KEY}
                  </li>
                  <li key={prop.time.toString()}>
                     Timestamp: {prop.time.toString()}
                  </li>
                  <li key={prop.networK_SID}>
                     Network SID: {prop.networK_SID}
                  </li>
                  <li key={prop.nealias}>NEALIAS: {prop.nealias}</li>
                  <li key={prop.netype}>NETYPE: {prop.netype}</li>
                  <li key={prop.rsL_INPUT_POWER}>
                     RSL_INPUT_POWER: {prop.rsL_INPUT_POWER}
                  </li>
                  <li key={prop.maX_RX_LEVEL}>
                     MAX_RX_LEVEL: {prop.maX_RX_LEVEL}
                  </li>
                  <li key={prop.rsL_DEVIATION}>
                     RSL_DEVIATION: {prop.rsL_DEVIATION}
                  </li>
               </ul>
            ))}
         </div>
      </>
   )
}

export default Chart
