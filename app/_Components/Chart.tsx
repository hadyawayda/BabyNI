type data = {
  DATETIME_KEY: number;
  TIME: Date;
  NETWORK_SID: number;
  NEALIAS: string;
  NETYPE: string;
  RSL_INPUT_POWER: number;
  MAX_RX_LEVEL: number;
  RSL_DEVIATION: number;
};

const Chart = ({ props }: { props: data[] }) => {
  const {
    DATETIME_KEY,
    TIME,
    NETWORK_SID,
    NEALIAS,
    NETYPE,
    RSL_INPUT_POWER,
    MAX_RX_LEVEL,
    RSL_DEVIATION,
  } = props[0];

  return (
    <>
      <div>Chart.exe</div>
    </>
  );
};

export default Chart;
