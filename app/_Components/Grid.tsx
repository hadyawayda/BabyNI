type Data = {
  DATETIME_KEY: number;
  TIME: Date;
  NETWORK_SID: number;
  NEALIAS: string;
  NETYPE: string;
  RSL_INPUT_POWER: number;
  MAX_RX_LEVEL: number;
  RSL_DEVIATION: number;
};

interface GridProps {
  props: Data[];
}

const Grid = ({ props }: GridProps) => {
  // const {
  //   DATETIME_KEY,
  //   TIME,
  //   NETWORK_SID,
  //   NEALIAS,
  //   NETYPE,
  //   RSL_INPUT_POWER,
  //   MAX_RX_LEVEL,
  //   RSL_DEVIATION,
  // } = props ;2

  return (
    <>
      <div>Grid.exe</div>
      {props.map((i: Data) => {
        return <p>{i.DATETIME_KEY}</p>;
      })}
    </>
  );
};

export default Grid;
