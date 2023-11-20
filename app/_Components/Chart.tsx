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

const ChartComponent = ({ props }: { props: Data[] }) => {
   return (
      <>
         <div className="grid grid-cols-4 gap-8"></div>
      </>
   )
}

export default ChartComponent
